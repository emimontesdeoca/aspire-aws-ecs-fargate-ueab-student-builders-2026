using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.Json;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisClient("cache");

var app = builder.Build();
app.MapDefaultEndpoints();           // /health and /alive

app.UseDefaultFiles();               // "/" -> wwwroot/index.html
app.UseStaticFiles();

var activitySource = new ActivitySource("CacaoStore.Api");
var meter = new Meter("CacaoStore.Api");
var ordersCreated = meter.CreateCounter<long>("cacao.orders.created", description: "Orders created");

// --- Catalogue: embedded JSON, loaded once ---------------------------------------------------
var products = LoadProducts();

// --- Endpoints -------------------------------------------------------------------------------

app.MapGet("/products", () => Results.Ok(products));

app.MapPost("/orders", async (NewOrder order, IConnectionMultiplexer redis, ILogger<Program> log) =>
{
    var product = products.FirstOrDefault(p => p.Id == order.ProductId);
    if (product is null || order.Quantity <= 0 || string.IsNullOrWhiteSpace(order.Customer))
    {
        return Results.BadRequest(new { error = "productId must exist, quantity > 0 and customer is required" });
    }

    using var activity = activitySource.StartActivity("CreateOrder");
    activity?.SetTag("order.product_id", order.ProductId);
    activity?.SetTag("order.quantity", order.Quantity);

    var db = redis.GetDatabase();

    // Redis as a tiny database: a counter for ids, a hash per order, a list with all ids (newest first).
    var id = await db.StringIncrementAsync("orders:next-id");
    var key = OrderKey(id);

    await db.HashSetAsync(key,
    [
        new HashEntry("id", id),
        new HashEntry("productId", product.Id),
        new HashEntry("product", product.Name),
        new HashEntry("quantity", order.Quantity),
        new HashEntry("customer", order.Customer.Trim()),
        new HashEntry("status", "pending"),
        new HashEntry("createdAt", DateTimeOffset.UtcNow.ToString("O")),
        // The W3C trace id of this request. The worker continues the same trace, so in the dashboard
        // you see HTTP POST -> Redis -> worker as ONE trace. That is the slide we build the talk around.
        new HashEntry("traceparent", Activity.Current?.Id ?? string.Empty),
    ]);
    await db.ListLeftPushAsync("orders:all", id);

    // The queue. Only the id travels; the worker reads the hash.
    await db.ListLeftPushAsync("orders", id);

    ordersCreated.Add(1);
    log.LogInformation("Order {OrderId} created for {Customer} and queued", id, order.Customer);

    return Results.Created($"/orders/{id}", new { id, status = "pending" });
});

app.MapGet("/orders", async (IConnectionMultiplexer redis) =>
{
    var db = redis.GetDatabase();
    var ids = await db.ListRangeAsync("orders:all", 0, 49);

    var orders = new List<Order>(ids.Length);
    foreach (var id in ids)
    {
        var order = ToOrder(await db.HashGetAllAsync(OrderKey((long)id)));
        if (order is not null)
        {
            orders.Add(order);
        }
    }
    return Results.Ok(orders);
});

app.MapGet("/orders/{id:long}", async (long id, IConnectionMultiplexer redis) =>
{
    var order = ToOrder(await redis.GetDatabase().HashGetAllAsync(OrderKey(id)));
    return order is null ? Results.NotFound() : Results.Ok(order);
});

app.Run();

// --- Helpers -----------------------------------------------------------------------------------

static string OrderKey(long id) => $"order:{id}";

static List<Product> LoadProducts()
{
    using var stream = typeof(Program).Assembly.GetManifestResourceStream("products.json")
        ?? throw new InvalidOperationException("products.json is not embedded in the assembly");
    return JsonSerializer.Deserialize<List<Product>>(stream, JsonSerializerOptions.Web) ?? [];
}

static Order? ToOrder(HashEntry[] hash)
{
    if (hash.Length == 0)
    {
        return null;
    }

    var fields = hash.ToDictionary(h => h.Name.ToString(), h => h.Value.IsNull ? null : h.Value.ToString());
    return new Order(
        long.Parse(fields["id"]!),
        fields["product"]!,
        int.Parse(fields["quantity"]!),
        fields["customer"]!,
        fields["status"]!,
        DateTimeOffset.Parse(fields["createdAt"]!),
        fields.TryGetValue("processedAt", out var processed) && !string.IsNullOrEmpty(processed)
            ? DateTimeOffset.Parse(processed)
            : null);
}

record Product(int Id, string Name, string Origin, decimal PriceUsd);
record NewOrder(int ProductId, int Quantity, string Customer);
record Order(long Id, string Product, int Quantity, string Customer, string Status, DateTimeOffset CreatedAt, DateTimeOffset? ProcessedAt);

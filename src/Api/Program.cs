using System.Net.Http;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisClient("cache");
builder.Services.AddHttpClient("sub-api", client =>
    client.BaseAddress = new Uri("https+http://sub-api"));

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGet("/database-info", async (
    IHttpClientFactory httpClientFactory,
    IConnectionMultiplexer redis) =>
{
    const string cacheKey = "demo:database-info";
    var cache = redis.GetDatabase();

    var cached = await cache.StringGetAsync(cacheKey);
    if (cached.HasValue)
    {
        return cached.ToString();
    }

    using var response = await httpClientFactory.CreateClient("sub-api")
        .GetAsync("/database-info");
    response.EnsureSuccessStatusCode();

    var value = await response.Content.ReadAsStringAsync();
    await cache.StringSetAsync(cacheKey, value, TimeSpan.FromMinutes(1));

    return value;
});

app.Run();

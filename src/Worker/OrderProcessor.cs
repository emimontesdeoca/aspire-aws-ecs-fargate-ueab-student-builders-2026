using System.Diagnostics;
using StackExchange.Redis;

namespace Worker;

/// <summary>
/// BRPOP on the "orders" list, then HSET status=processed on the order hash.
/// Each order becomes a span that continues the trace the API started, so the dashboard shows
/// one trace: HTTP POST /orders -> Redis -> ProcessOrder.
/// </summary>
public sealed class OrderProcessor(IConnectionMultiplexer redis, ILogger<OrderProcessor> log) : BackgroundService
{
    private static readonly ActivitySource ActivitySource = new("CacaoStore.Worker");

    // StackExchange.Redis multiplexes one connection, so it does not expose blocking commands directly.
    // Execute() sends BRPOP anyway. Keep the timeout short (1 s) so it stays under the client's
    // 5 s async timeout and the health check PING is never blocked for long.
    private const int BlockSeconds = 1;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var db = redis.GetDatabase();
        log.LogInformation("Worker started. Waiting on list 'orders'");

        while (!stoppingToken.IsCancellationRequested)
        {
            RedisResult result;
            try
            {
                result = await db.ExecuteAsync("BRPOP", "orders", BlockSeconds);
            }
            catch (RedisException ex)
            {
                log.LogWarning(ex, "BRPOP failed, retrying in 2 s");
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                continue;
            }

            if (result.IsNull)
            {
                continue;   // timeout, nothing queued
            }

            var pair = (RedisResult[])result!;   // BRPOP returns [listName, value]
            var orderId = (long)pair[1];

            try
            {
                await ProcessAsync(db, orderId, stoppingToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                // In a demo we want to see the error and keep going.
                log.LogError(ex, "Error processing order {OrderId}", orderId);
            }
        }
    }

    private async Task ProcessAsync(IDatabase db, long orderId, CancellationToken ct)
    {
        var key = $"order:{orderId}";

        // Continue the trace the API started: the hash carries the W3C traceparent of the POST.
        var traceparent = (string?)await db.HashGetAsync(key, "traceparent");
        ActivityContext.TryParse(traceparent, null, out var parentContext);

        using var activity = ActivitySource.StartActivity("ProcessOrder", ActivityKind.Consumer, parentContext);
        activity?.SetTag("order.id", orderId);

        var customer = (string?)await db.HashGetAsync(key, "customer") ?? "unknown";

        await Task.Delay(800, ct);   // "processing": packing, invoicing...

        await db.HashSetAsync(key,
        [
            new HashEntry("status", "processed"),
            new HashEntry("processedAt", DateTimeOffset.UtcNow.ToString("O")),
        ]);

        log.LogInformation("Order {OrderId} processed for {Customer}", orderId, customer);
    }
}

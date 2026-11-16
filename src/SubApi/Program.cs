using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDataSource("cacao");

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGet("/database-info", async (NpgsqlDataSource dataSource) =>
{
    await using var command = dataSource.CreateCommand(
        "SELECT current_database() || ' | ' || CURRENT_TIMESTAMP::text");
    return (string?)await command.ExecuteScalarAsync()
        ?? throw new InvalidOperationException("PostgreSQL returned no database information.");
})
    .WithName("GetDatabaseInfo");

app.Run();

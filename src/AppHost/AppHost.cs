var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("compose");
var database = builder.AddPostgres("postgres").AddDatabase("cacao");
var cache = builder.AddRedis("cache");

var subApi = builder.AddProject<Projects.SubApi>("sub-api")
    .WithReference(database)
    .WaitFor(database)
    .WithHttpEndpoint(port: 8081, targetPort: 18081)
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.Api>("api")
    .WithReference(subApi)
    .WithReference(cache)
    .WaitFor(subApi)
    .WaitFor(cache)
    .WithHttpEndpoint(port: 8080, targetPort: 18080)
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health");

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("compose");
var cache = builder.AddRedis("cache");

builder.AddProject<Projects.Api>("api")
    .WithReference(cache)
    .WaitFor(cache)
    .WithHttpEndpoint(port: 8080, targetPort: 8080)
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.Worker>("worker")
    .WithReference(cache)
    .WaitFor(cache);

builder.Build().Run();

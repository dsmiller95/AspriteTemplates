var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.OrderManager_ApiService>("apiservice");

builder.AddProject<Projects.OrderManager_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();

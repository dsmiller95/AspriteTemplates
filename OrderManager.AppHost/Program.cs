var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql").WithDataVolume("sql-server-data");
var sqldb = sql.AddDatabase("sqldb");

var apiService = builder.AddProject<Projects.OrderManager_ApiService>("apiservice")
        .WithReference(sqldb)
    ;

builder.AddProject<Projects.OrderManager_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();

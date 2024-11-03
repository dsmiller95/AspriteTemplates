using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManager.ApiService;
using OrderManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddSqlServerDbContext<OrderManagerDbContext>("sqldb");

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddScoped<DataItemRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// run ef migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderManagerDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGet("/dataItem", async (DataItemRepository repository) =>
{
    return await repository.GetDataItems();
});
app.MapGet("/dataItem/{id}", async (DataItemRepository repository, [FromRoute] Guid id) =>
{
    return await repository.GetDataItem(id);
});
app.MapPost("/dataItem", async (DataItemRepository repository, [FromBody] DataItem dataItem) =>
{
    return await repository.CreateDataItem(dataItem);
});
app.MapPut("/dataItem", async (DataItemRepository repository, [FromBody] DataItem dataItem) =>
{
    return await repository.UpdateDataItem(dataItem);
});
app.MapDelete("/dataItem/{id}", async (DataItemRepository repository, [FromRoute] Guid id) =>
{
    await repository.DeleteDataItem(id);
    return Results.NoContent();
});

app.MapDefaultEndpoints();

app.Run();

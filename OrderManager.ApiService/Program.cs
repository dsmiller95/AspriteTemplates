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

builder.Services.AddScoped<ItemRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// run ef migrations on startup in
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderManagerDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGet("/item", async (ItemRepository repository) =>
{
    return await repository.GetOrders();
});
app.MapGet("/item/{id}", async (ItemRepository repository, [FromRoute] Guid id) =>
{
    return await repository.GetOrder(id);
});
app.MapPost("/item", async (ItemRepository repository, [FromBody] Item order) =>
{
    return repository.CreateOrder(order);
});
app.MapPut("/item", async (ItemRepository repository, [FromBody] Item order) =>
{
    return repository.UpdateOrder(order);
});
app.MapDelete("/item/{id}", async (ItemRepository repository, [FromRoute] Guid id) =>
{
    await repository.DeleteOrder(id);
    return Results.NoContent();
});

app.MapDefaultEndpoints();

app.Run();

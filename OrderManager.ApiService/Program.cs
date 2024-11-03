using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManager.ApiService;
using OrderManager.ApiService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddSqlServerDbContext<OrderManagerDbContext>("sqldb");

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<OrderRepository>();

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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});


app.MapGet("/products", async (ProductRepository repository) =>
{
    return await repository.GetProducts();
});
app.MapGet("/products/{id}", async (ProductRepository repository, [FromRoute] Guid id) =>
{
    return await repository.GetProduct(id);
});
app.MapPost("/products", async (ProductRepository repository, [FromBody] Product product) =>
{
    return await repository.CreateProduct(product);
});


app.MapGet("/orders", async (OrderRepository repository) =>
{
    return await repository.GetOrders();
});
app.MapGet("/orders/{id}", async (OrderRepository repository, [FromRoute] Guid id) =>
{
    return await repository.GetOrder(id);
});
app.MapPost("/orders", async (OrderRepository repository, [FromBody] Order order) =>
{
    return repository.CreateOrder(order);
});


app.MapDefaultEndpoints();

app.Run();

namespace OrderManager.ApiService
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}

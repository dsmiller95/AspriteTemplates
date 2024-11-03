using Microsoft.EntityFrameworkCore;
using OrderManager.ApiService.Models;

namespace OrderManager.ApiService;

public class OrderManagerDbContext : DbContext
{
    public OrderManagerDbContext(DbContextOptions<OrderManagerDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}
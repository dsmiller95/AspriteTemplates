using Microsoft.EntityFrameworkCore;
using OrderManager.Models;

namespace OrderManager.ApiService;

public class OrderManagerDbContext : DbContext
{
    public OrderManagerDbContext(DbContextOptions<OrderManagerDbContext> options) : base(options)
    {
    }

    public DbSet<Item> Items { get; set; }
}
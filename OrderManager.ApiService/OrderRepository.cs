using Microsoft.EntityFrameworkCore;
using OrderManager.ApiService.Models;

namespace OrderManager.ApiService;

public class OrderRepository(OrderManagerDbContext dbContext)
{
    public async Task<List<Order>> GetOrders()
    {
        return await dbContext.Orders.ToListAsync();
    }
    
    public async Task<Order?> GetOrder(Guid id)
    {
        return await dbContext.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<Order> CreateOrder(Order order)
    {
        var result = await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }
    
}
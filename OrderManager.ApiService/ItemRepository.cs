using Microsoft.EntityFrameworkCore;
using OrderManager.Models;

namespace OrderManager.ApiService;

public class ItemRepository(OrderManagerDbContext dbContext)
{
    public async Task<List<Item>> GetOrders()
    {
        return await dbContext.Items.ToListAsync();
    }
    
    public async Task<Item?> GetOrder(Guid id)
    {
        return await dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<Item> CreateOrder(Item item)
    {
        var result = await dbContext.Items.AddAsync(item);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<Item> UpdateOrder(Item item)
    {
        var result = dbContext.Items.Update(item);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task DeleteOrder(Guid id)
    {
        var item = await dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
        if(item != null)
        {
            dbContext.Items.Remove(item);
            await dbContext.SaveChangesAsync();
        }
    }
}
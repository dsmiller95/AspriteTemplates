using Microsoft.EntityFrameworkCore;
using OrderManager.Models;

namespace OrderManager.ApiService;

public class DataItemRepository(OrderManagerDbContext dbContext)
{
    public async Task<List<DataItem>> GetDataItems()
    {
        return await dbContext.DataItems.ToListAsync();
    }
    
    public async Task<DataItem?> GetDataItem(Guid id)
    {
        return await dbContext.DataItems.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<DataItem> CreateDataItem(DataItem dataItem)
    {
        var result = await dbContext.DataItems.AddAsync(dataItem);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<DataItem> UpdateDataItem(DataItem dataItem)
    {
        var result = dbContext.DataItems.Update(dataItem);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task DeleteDataItem(Guid id)
    {
        var dataItem = await dbContext.DataItems.FirstOrDefaultAsync(x => x.Id == id);
        if(dataItem != null)
        {
            dbContext.DataItems.Remove(dataItem);
            await dbContext.SaveChangesAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using OrderManager.ApiService.Models;

namespace OrderManager.ApiService;

public class ProductRepository(OrderManagerDbContext dbContext)
{
    public async Task<List<Product>> GetProducts()
    {
        return await dbContext.Products.ToListAsync();
    }
    
    public async Task<Product?> GetProduct(Guid id)
    {
        return await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<Product> CreateProduct(Product product)
    {
        var result = await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }
    
}
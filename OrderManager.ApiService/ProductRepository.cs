using OrderManager.ApiService.Models;

namespace OrderManager.ApiService;

public class ProductRepository
{
    public async Task<List<Product>> GetProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "Description 1",
                Price = 10.00m,
                StockQuantity = 100
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Description = "Description 2",
                Price = 20.00m,
                StockQuantity = 200
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 3",
                Description = "Description 3",
                Price = 30.00m,
                StockQuantity = 300
            }
        };
    }
    
    public async Task<Product> GetProduct(Guid id)
    {
        return new Product
        {
            Id = id,
            Name = "Product 1",
            Description = "Description 1",
            Price = 10.00m,
            StockQuantity = 100
        };
    }
    
    public async Task<Product> CreateProduct(Product product)
    {
        return product;
    }
    
}
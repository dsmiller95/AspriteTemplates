using OrderManager.ApiService.Models;

namespace OrderManager.ApiService;

public class OrderRepository
{
    public async Task<List<Order>> GetOrders()
    {
        return new List<Order>
        {
            new Order
            {
                Id = Guid.NewGuid(),
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 1
                    },
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 2
                    }
                }
            },
            new Order
            {
                Id = Guid.NewGuid(),
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 3
                    },
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 4
                    }
                }
            }
        };
    }
    
    public async Task<Order> GetOrder(Guid id)
    {
        return new Order
        {
            Id = id,
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = Guid.NewGuid(),
                    Quantity = 1
                },
                new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = Guid.NewGuid(),
                    Quantity = 2
                }
            }
        };
    }
    
    public async Task<Order> CreateOrder(Order order)
    {
        return order;
    }
    
}
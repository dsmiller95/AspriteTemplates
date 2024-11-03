namespace OrderManager.ApiService.Models;

public record Order
{
    public Guid Id { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];
}
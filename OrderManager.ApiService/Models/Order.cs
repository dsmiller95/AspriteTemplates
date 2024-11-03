namespace OrderManager.ApiService.Models;

public record Order
{
    public Guid Id { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
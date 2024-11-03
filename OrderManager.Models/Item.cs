namespace OrderManager.Models;

public record Item
{
    public Guid Id { get; set; }
    public string ItemName { get; set; } = string.Empty;
}
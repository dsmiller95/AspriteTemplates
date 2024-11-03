namespace OrderManager.Models;

public record DataItem
{
    public Guid Id { get; set; }
    public string DataItemName { get; set; } = string.Empty;
}
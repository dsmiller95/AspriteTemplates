using System.Diagnostics;
using OrderManager.Models;

namespace OrderManager.Web;

public class OrderManagerApiClient(HttpClient httpClient)
{
    public async Task<Item[]> GetItemsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        var productList =  await httpClient.GetFromJsonAsAsyncEnumerable<Item>("/item", cancellationToken)
            .Take(maxItems)
            .ToListAsync(cancellationToken);
        return productList.ToArray()!;
    }
    
    public async Task DeleteItemAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await httpClient.DeleteAsync($"/item/{id}", cancellationToken);
    }
    
    public async Task<Item> CreateItemAsync(Item item, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/item", item, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<Item>(cancellationToken: cancellationToken);
        if(result == null) throw new Exception("Failed to create item");
        return result;
    }
}

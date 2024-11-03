using System.Diagnostics;
using OrderManager.Models;

namespace OrderManager.Web;

public class OrderManagerApiClient(HttpClient httpClient)
{
    public async Task<Item[]> GetItemsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        var productList =  await httpClient.GetFromJsonAsAsyncEnumerable<Item>("/products", cancellationToken)
            .Take(maxItems)
            .ToListAsync(cancellationToken);
        return productList.ToArray()!;
    }
    
    
}

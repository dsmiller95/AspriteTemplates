using System.Diagnostics;
using OrderManager.ApiService.Models;

namespace OrderManager.Web;

public class OrdersApiClient(HttpClient httpClient)
{
    public async Task<Product[]> GetProductAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        var productList =  await httpClient.GetFromJsonAsAsyncEnumerable<Product>("/products", cancellationToken)
            .Take(maxItems)
            .ToListAsync(cancellationToken);
        return productList.ToArray();
    }
    
    
}

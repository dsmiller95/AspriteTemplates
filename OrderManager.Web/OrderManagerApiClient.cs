using System.Diagnostics;
using OrderManager.Models;

namespace OrderManager.Web;

public class OrderManagerApiClient(HttpClient httpClient)
{
    public async Task<DataItem[]> GetDataItemsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        var dataItemList =  await httpClient.GetFromJsonAsAsyncEnumerable<DataItem>("/dataItem", cancellationToken)
            .Take(maxItems)
            .ToListAsync(cancellationToken);
        return dataItemList.ToArray()!;
    }
    
    public async Task DeleteDataItemAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await httpClient.DeleteAsync($"/dataItem/{id}", cancellationToken);
    }
    
    public async Task<DataItem> CreateDataItemAsync(DataItem dataItem, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/dataItem", dataItem, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<DataItem>(cancellationToken: cancellationToken);
        if(result == null) throw new Exception("Failed to create dataItem");
        return result;
    }
}

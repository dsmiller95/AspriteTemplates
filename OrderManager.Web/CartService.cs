using OrderManager.ApiService.Models;

namespace OrderManager.Web;

public class CartModel
{
    public Dictionary<Guid, CartItem> CartItems { get; set; } = new();
    
    public CartItem? GetItem(Guid productId)
    {
        return CartItems.GetValueOrDefault(productId);
    }
    
    public int TotalItems => CartItems.Values.Sum(x => x.Quantity);
    
    public bool CanAddToCart(Product product, int quantity)
    {
        var existingInCart = GetItem(product.Id)?.Quantity ?? 0;
        return existingInCart + quantity <= product.StockQuantity;
    }
    
    public void AddToCart(Product product, int quantity)
    {
        if(CartItems.TryGetValue(product.Id, out var cartItem))
        {
            CartItems[product.Id] = new CartItem(product, cartItem.Quantity + quantity);
        }
        else
        {
            CartItems.Add(product.Id, new CartItem(product, quantity));
        }
    }
    
    public int GetAmountInCart(Product product)
    {
        return GetItem(product.Id)?.Quantity ?? 0;
    }
}

public record CartItem(Product Item, int Quantity);

public class CartService
{
    private CartModel InMemoryCartModel { get; set; } = new();
    public async Task<CartModel> GetCartAsync()
    {
        return InMemoryCartModel;
    }

    public async Task SetCartAsync(CartModel cartModel)
    {
        InMemoryCartModel = cartModel;
    }
}

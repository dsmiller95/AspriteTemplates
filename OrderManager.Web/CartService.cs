using OrderManager.ApiService.Models;

namespace OrderManager.Web;

public class Cart
{
    public Dictionary<Guid, CartItem> CartItems { get; set; } = new();
    
    public CartItem? GetItem(Guid productId)
    {
        return CartItems.GetValueOrDefault(productId);
    }
    
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
    private Cart InMemoryCart { get; set; } = new();
    public async Task<Cart> GetCartAsync()
    {
        return InMemoryCart;
    }

    public async Task SetCartAsync(Cart cart)
    {
        InMemoryCart = cart;
    }
}

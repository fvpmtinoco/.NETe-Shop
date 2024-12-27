namespace BasketAPI.Models;

public class ShoppingCart
{
    public string Username { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Quantity * x.Price);

    public ShoppingCart(string userName)
    {
        Username = userName;
    }

    public ShoppingCart() { }
}
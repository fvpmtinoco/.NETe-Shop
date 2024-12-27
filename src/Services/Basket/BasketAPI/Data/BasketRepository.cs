using BasketAPI.Exceptions;
using BasketAPI.Models;

namespace BasketAPI.Data;

public class BasketRepository(IDocumentSession documentSession) : IBasketRepository
{
    public async Task<bool> DeleteBasketAsync(string username, CancellationToken cancellationToken = default)
    {
        documentSession.Delete<ShoppingCart>(username);
        await documentSession.SaveChangesAsync();
        return true;
    }

    public async Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellationToken = default)
    {
        ShoppingCart? basket = await documentSession.LoadAsync<ShoppingCart>(username, cancellationToken);
        return basket is null ? throw new BasketNotFoundException(username) : basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        documentSession.Store<ShoppingCart>(cart);
        await documentSession.SaveChangesAsync(cancellationToken);
        return cart;
    }
}

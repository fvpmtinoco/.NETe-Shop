using BasketAPI.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BasketAPI.Data;

public class CacheBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
{
    public async Task<bool> DeleteBasketAsync(string username, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasketAsync(username, cancellationToken);

        await cache.RemoveAsync(username, cancellationToken);

        return true;
    }

    public async Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellationToken = default)
    {
        var cacheBasket = await cache.GetStringAsync(username, cancellationToken);

        if (!string.IsNullOrEmpty(cacheBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;

        var basket = await basketRepository.GetBasketAsync(username, cancellationToken);

        await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        var storedCart = await basketRepository.StoreBasketAsync(cart, cancellationToken);

        await cache.SetStringAsync(cart.Username, JsonSerializer.Serialize(storedCart), cancellationToken);

        return storedCart;
    }
}

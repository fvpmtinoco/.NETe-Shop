using BasketAPI.Data;
using BasketAPI.Models;

namespace BasketAPI.Basket.GetBasket;

public record GetBasketQuery(string Username) : IRequest<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IBasketRepository repository) : IRequestHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        ShoppingCart basket = await repository.GetBasketAsync(query.Username, cancellationToken);

        return new GetBasketResult(basket);
    }
}
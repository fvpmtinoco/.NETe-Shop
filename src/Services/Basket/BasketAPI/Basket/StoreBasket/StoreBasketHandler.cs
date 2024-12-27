using BasketAPI.Data;
using BasketAPI.Models;
using FluentValidation;

namespace BasketAPI.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : IRequest<StoreBasketResult>;

public record StoreBasketResult(string Username);

public class StoreBasketCommandHandler(IBasketRepository repository) : IRequestHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart result = await repository.StoreBasketAsync(command.Cart, cancellationToken);
        return new StoreBasketResult(result.Username);
    }
}

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username can not be empty");
    }
}
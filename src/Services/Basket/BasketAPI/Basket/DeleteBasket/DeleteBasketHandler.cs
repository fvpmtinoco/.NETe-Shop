using BasketAPI.Data;
using FluentValidation;

namespace BasketAPI.Basket.DeleteBasket;

public record DeleteBasketCommand(string Username) : IRequest<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandHandler(IBasketRepository repository) : IRequestHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        bool result = await repository.DeleteBasketAsync(command.Username, cancellationToken);
        return new DeleteBasketResult(result);
    }
}

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username can not be empty");
    }
}
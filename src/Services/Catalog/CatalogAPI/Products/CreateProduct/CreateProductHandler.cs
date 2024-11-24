using BuildingBlocks.CQRS;
using MediatR;

namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        // Business logic to create a product
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

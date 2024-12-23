using CatalogAPI.Exceptions;

namespace CatalogAPI.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IRequest<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandHandler(IDocumentSession documentSession, ILogger<DeleteProductCommandHandler> logger) : IRequestHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductCommandHandler.Handle called with {@command}", command);
            var product = await documentSession.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new ProductNotFoundException(command.Id.ToString());

            documentSession.Delete(product);
            await documentSession.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}

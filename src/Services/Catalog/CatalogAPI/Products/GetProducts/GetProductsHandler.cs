
namespace CatalogAPI.Products.GetProducts
{
    public record class GetProductsQuery() : IQuery<GetProductResult>;
    public record class GetProductResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler(IDocumentSession documentSession, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle called with {@query}", query);
            var products = await documentSession.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductResult(products);
        }
    }
}

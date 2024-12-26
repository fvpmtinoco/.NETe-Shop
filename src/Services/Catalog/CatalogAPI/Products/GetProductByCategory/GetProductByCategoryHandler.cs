
namespace CatalogAPI.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IRequest<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryHandler(IDocumentSession session) : IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Product> products = await session.Query<Product>()
            .Where(p => p.Categories.Contains(request.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
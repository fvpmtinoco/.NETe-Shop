
using Marten.Pagination;

namespace CatalogAPI.Products.GetProducts;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductsQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        IPagedList<Product> products = await documentSession.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetProductResult(products);
    }
}

namespace CatalogAPI.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender, CancellationToken ct) =>
        {
            GetProductByCategoryResult result = await sender.Send(new GetProductByCategoryQuery(category), ct);
            GetProductByCategoryResponse response = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(result);
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get products by category");
    }
}

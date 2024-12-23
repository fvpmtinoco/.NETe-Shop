
namespace CatalogAPI.Products.UpdateProduct
{
    public class UpdateProductEndpoint : ICarterModule
    {
        public record class UpdateProductRequest(
            Guid Id,
            string Name,
            List<string> Categories,
            string Description,
            string ImageFile,
            decimal Price);

        public record UpdateProductResponse(bool IsSuccess);

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async(UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update a product");
        }
    }
}

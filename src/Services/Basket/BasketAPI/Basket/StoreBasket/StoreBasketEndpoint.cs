using BasketAPI.Models;
using Carter;
using Mapster;
using MediatR;

namespace BasketAPI.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);

public record StoreBasketResponse(string Username);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            StoreBasketResult result = await sender.Send(new StoreBasketCommand(request.Cart));
            StoreBasketResponse response = result.Adapt<StoreBasketResponse>();
            return Results.Created($"/basket/{response.Username}", response);
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store basket");
    }
}

using BuildingBlocks.Exceptions;

namespace BasketAPI.Exceptions;

public class BasketNotFoundException(string username) : NotFoundException("Basket", username) { }
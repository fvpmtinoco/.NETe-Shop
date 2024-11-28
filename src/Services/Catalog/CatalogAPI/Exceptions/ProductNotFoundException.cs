namespace CatalogAPI.Exceptions
{
    public class ProductNotFoundException(string error) : Exception(error) { }
}
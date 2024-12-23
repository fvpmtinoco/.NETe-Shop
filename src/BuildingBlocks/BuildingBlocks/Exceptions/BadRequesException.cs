namespace BuildingBlocks.Exceptions;
public class BadRequesException : Exception
{
    public BadRequesException(string message) : base(message) { }

    public BadRequesException(string message, string details) : base(message)
    {
        Details = details;
    }

    public string? Details { get; }
}

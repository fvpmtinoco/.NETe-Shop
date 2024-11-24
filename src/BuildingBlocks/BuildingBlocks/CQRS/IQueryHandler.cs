using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        // Redundant as it is already being enforced in IQuery
        where TResponse : notnull
    { }
}

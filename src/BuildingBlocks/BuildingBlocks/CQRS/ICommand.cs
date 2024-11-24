using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }

    // Although not necessary to specify the return type as Unit (aka void),
    // it is a good practice to do so for readability
    public interface ICommand : IRequest<Unit> { }
}

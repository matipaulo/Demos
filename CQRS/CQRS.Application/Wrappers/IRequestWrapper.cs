using MediatR;

namespace CQRS.Application.Wrappers
{
    public interface IRequestWrapper<TResponse> : IRequest<Response<TResponse>>
    {
    }

    public interface IHandlerWrapper<in TRequest, TResponse> : IRequestHandler<TRequest, Response<TResponse>>
        where TRequest : IRequestWrapper<TResponse>
    {
    }
}
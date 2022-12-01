using MediatR;

namespace ProductCatalog.Application.Abstractions;

public  interface ICommand<out TResponse> : IRequest<TResponse>
{
}
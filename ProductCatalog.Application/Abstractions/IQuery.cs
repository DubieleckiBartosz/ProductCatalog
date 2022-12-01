using MediatR;

namespace ProductCatalog.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
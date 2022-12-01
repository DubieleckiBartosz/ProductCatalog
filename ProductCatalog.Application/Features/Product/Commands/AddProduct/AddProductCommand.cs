using MediatR;
using ProductCatalog.Application.Abstractions;
using ProductCatalog.Application.Parameters.ProductParameters;

namespace ProductCatalog.Application.Features.Product.Commands.AddProduct;

public record AddProductCommand(string Name, decimal Price) : ICommand<Unit>
{
    public static AddProductCommand Create(AddProductCommandParameters parameters)
    {
        return new AddProductCommand(parameters.Name, parameters.Price);
    }
}
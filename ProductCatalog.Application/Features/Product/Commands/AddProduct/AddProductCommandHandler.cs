using MediatR;
using ProductCatalog.Application.Abstractions;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Exceptions;
using ProductCatalog.Application.Interfaces.Repositories;
using ProductCatalog.Application.Logging;

namespace ProductCatalog.Application.Features.Product.Commands.AddProduct;

public class AddProductCommandHandler : ICommandHandler<AddProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly ILoggerManager<AddProductCommandHandler> _logger;

    public AddProductCommandHandler(IProductRepository productRepository, ILoggerManager<AddProductCommandHandler> logger)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetProductByNameAsync(request.Name);
        if (existingProduct != null)
        {
            throw new BadRequestException(Strings.ProductAlreadyExistsMessage, Strings.ProductAlreadyExistsTitle);
        }

        var newProduct = Domain.Entities.Product.Create(request.Name, request.Price);

        await _productRepository.AddAsync(product: newProduct);

        _logger.LogInformation(new
        {
            Message = "New product has been created",
            ProductCode = newProduct.Code
        });

        return Unit.Value;
    }
}
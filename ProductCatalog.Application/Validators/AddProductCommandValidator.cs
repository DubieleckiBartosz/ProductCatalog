using FluentValidation;
using ProductCatalog.Application.Features.Product.Commands.AddProduct;

namespace ProductCatalog.Application.Validators;

public class AddProductCommandValidator : AbstractValidatorNotNull<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(_ => _.Name).NotEmpty().NotNull();
        RuleFor(_ => _.Price).GreaterThan(0);
    }
}
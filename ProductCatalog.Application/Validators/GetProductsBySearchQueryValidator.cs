using FluentValidation;
using ProductCatalog.Application.Features.Product.Queries.GetProductsBySearch; 

namespace ProductCatalog.Application.Validators;

public class GetProductsBySearchQueryValidator : AbstractValidator<GetProductsBySearchQuery>
{
    private readonly string[] _availableName = new[]
    {
        "Price", "Name", "Created"
    };

    public GetProductsBySearchQueryValidator()
    {
        this.When(_ => _.SortModel?.Name != null,
            () => this.RuleFor(r => r.SortModel).SetValidator(new SortValidator(this._availableName)!));
    }
}
using FluentValidation; 
using ProductCatalog.Application.Search;

namespace ProductCatalog.Application.Validators;

public class SortValidator : AbstractValidator<SortModel>
{
    public SortValidator(string[] availableNames)
    {
        When(r => r.Name != null, () =>
        {
            RuleFor(r => r.Name).Custom((sort, context) =>
            {
                if (!string.IsNullOrWhiteSpace(sort) &&
                    !availableNames.Contains(sort, StringComparer.OrdinalIgnoreCase))
                    context.AddFailure("SortName",
                        $"Sort name must in [{string.Join(", ", availableNames)}]");
            });
        });
    }
}
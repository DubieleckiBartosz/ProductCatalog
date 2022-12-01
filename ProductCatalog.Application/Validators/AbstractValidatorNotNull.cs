using FluentValidation;
using FluentValidation.Results;
using ProductCatalog.Application.Constants;

namespace ProductCatalog.Application.Validators;

public class AbstractValidatorNotNull<T> : AbstractValidator<T>
{
    public override ValidationResult Validate(ValidationContext<T> context)
    {
        return context.InstanceToValidate == null
            ? new ValidationResult(new[] { new ValidationFailure(nameof(T), Strings.ObjectCannotBeNull) })
            : base.Validate(context);
    }
}
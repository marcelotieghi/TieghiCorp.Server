using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Location.Create;

public sealed class CreateLocationValidator : AbstractValidator<CreateLocationRequest>
{
    public CreateLocationValidator()
    {
        RuleFor(l => l.Name)
            .ApplyStringValidationRules();
    }
}
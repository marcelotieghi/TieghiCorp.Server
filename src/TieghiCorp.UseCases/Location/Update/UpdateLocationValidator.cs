using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Location.Update;

public sealed class UpdateLocationValidator : AbstractValidator<UpdateLocationRequest>
{
    public UpdateLocationValidator()
    {
        RuleFor(l => l.Id)
           .ApplyIntValidationRules();

        RuleFor(l => l.Name)
            .ApplyStringValidationRules();
    }
}
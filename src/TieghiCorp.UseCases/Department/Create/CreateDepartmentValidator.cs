using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Department.Create;

public sealed class CreateDepartmentValidator : AbstractValidator<CreateDepartmentRequest>
{
    public CreateDepartmentValidator()
    {
        RuleFor(d => d.Name)
            .ApplyStringValidationRules();

        RuleFor(d => d.LocationId)
            .ApplyIntValidationRules();
    }
}
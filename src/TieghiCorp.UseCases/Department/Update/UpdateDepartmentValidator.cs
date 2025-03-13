using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;
using TieghiCorp.UseCases.Department.Update;

namespace TieghiCorp.UseCases.Department;

public sealed class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentRequest>
{
    public UpdateDepartmentValidator()
    {
        RuleFor(d => d.Id)
            .ApplyIntValidationRules();

        RuleFor(d => d.Name)
            .ApplyStringValidationRules();

        RuleFor(d => d.LocationId)
            .ApplyIntValidationRules();
    }
}
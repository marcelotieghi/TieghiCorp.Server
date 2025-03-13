using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Department.Delete;

public sealed class DeleteDepartmentValidator : AbstractValidator<DeleteDepartmentRequest>
{
    public DeleteDepartmentValidator()
    {
        RuleFor(d => d.Id)
            .ApplyIntValidationRules();
    }
}
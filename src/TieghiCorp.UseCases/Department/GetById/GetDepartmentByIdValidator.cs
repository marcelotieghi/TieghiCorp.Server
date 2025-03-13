using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Department.GetById;

public sealed class GetDepartmentByIdValidator : AbstractValidator<GetDepartmentByIdRequest>
{
    public GetDepartmentByIdValidator()
    {
        RuleFor(d => d.Id)
            .ApplyIntValidationRules();
    }
}
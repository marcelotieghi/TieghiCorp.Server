using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Department.GetAll;

public sealed class GetAllDepartmentValidator : AbstractValidator<GetAllDepartmentRequest>
{
    public GetAllDepartmentValidator()
    {
        RuleFor(d => d.Page)
            .ApplyIntValidationRules();

        RuleFor(d => d.PageSize)
            .ApplyIntValidationRules()
            .InclusiveBetween(1, 100)
            .WithMessage("{PropertyName} must be between 1 and 100.");

        RuleFor(d => d.SearchTerm)
            .MaximumLength(100)
            .WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(d => d.SortField)
            .ApplySortFieldValidation(["id", "name", "locationid"]);

        RuleFor(d => d.SortDirection)
            .ApplySortDirectionValidation();
    }
}
using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Personnel.GetAll;

public sealed class GetAllPersonnelValidator : AbstractValidator<GetAllPersonnelRequest>
{
    public GetAllPersonnelValidator()
    {
        RuleFor(p => p.Page)
            .ApplyIntValidationRules();

        RuleFor(p => p.PageSize)
            .ApplyIntValidationRules()
            .InclusiveBetween(1, 100)
            .WithMessage("{PropertyName} must be between 1 and 100.");

        RuleFor(p => p.SearchTerm)
            .MaximumLength(100)
            .WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.SortField)
            .ApplySortFieldValidation(["id", "firstname", "lastname", "email", "jobtitle", "departmentid"]);

        RuleFor(p => p.SortDirection)
            .ApplySortDirectionValidation();
    }
}
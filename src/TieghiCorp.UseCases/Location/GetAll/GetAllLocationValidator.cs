using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Location.GetAll;

public sealed class GetAllLocationValidator : AbstractValidator<GetAllLocationRequest>
{
    public GetAllLocationValidator()
    {
        RuleFor(l => l.Page)
            .ApplyIntValidationRules();

        RuleFor(l => l.PageSize)
            .ApplyIntValidationRules()
            .InclusiveBetween(1, 100)
            .WithMessage("{PropertyName} must be between 1 and 100.");

        RuleFor(l => l.SearchTerm)
            .MaximumLength(100)
            .WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(l => l.SortField)
            .ApplySortFieldValidation(["id", "name"]);

        RuleFor(l => l.SortDirection)
            .ApplySortDirectionValidation();
    }
}
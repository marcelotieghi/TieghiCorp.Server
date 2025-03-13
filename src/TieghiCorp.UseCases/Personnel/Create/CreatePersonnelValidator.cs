using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Personnel.Create;

public sealed class CreatePersonnelValidator : AbstractValidator<CreatePersonnelRequest>
{
    public CreatePersonnelValidator()
    {
        RuleFor(p => p.Firstname)
            .ApplyStringValidationRules();

        RuleFor(p => p.Lastname)
            .ApplyStringValidationRules();

        RuleFor(p => p.Email)
            .EmailAddress();

        RuleFor(p => p.JobTitle)
            .ApplyStringValidationRules();

        RuleFor(p => p.DepartmentId)
            .ApplyIntValidationRules();
    }
}
using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Personnel.Update;

public sealed class UpdatePersonnelValidator : AbstractValidator<UpdatePersonnelRequest>
{
    public UpdatePersonnelValidator()
    {
        RuleFor(p => p.Id)
            .ApplyIntValidationRules();

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
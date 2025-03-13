using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Personnel.Delete;

public sealed class DeletePersonnelValidator : AbstractValidator<DeletePersonnelRequest>
{
    public DeletePersonnelValidator()
    {
        RuleFor(p => p.Id)
            .ApplyIntValidationRules();
    }
}
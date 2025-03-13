using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Location.Delete;

public sealed class DeleteLocationValidator : AbstractValidator<DeleteLocationRequest>
{
    public DeleteLocationValidator()
    {
        RuleFor(l => l.Id)
            .ApplyIntValidationRules();
    }
}
using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Personnel.GetById;

public sealed class GetPersonnelByIdValidator : AbstractValidator<GetPersonnelByIdRequest>
{
    public GetPersonnelByIdValidator()
    {
        RuleFor(p => p.Id)
            .ApplyIntValidationRules();
    }
}
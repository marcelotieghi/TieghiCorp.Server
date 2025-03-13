using FluentValidation;
using TieghiCorp.UseCases.Common.Extensions;

namespace TieghiCorp.UseCases.Location.GetById;

public sealed class GetLocationByIdValidator : AbstractValidator<GetLocationByIdRequest>
{
    public GetLocationByIdValidator()
    {
        RuleFor(l => l.Id)
            .ApplyIntValidationRules();
    }
}
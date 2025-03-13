using FluentValidation;

namespace TieghiCorp.UseCases.Common.Extensions;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, int> ApplyIntValidationRules<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be a positive integer.");
    }

    public static IRuleBuilderOptions<T, string> ApplyStringValidationRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.")
            .Length(3, 100)
                .WithMessage("{PropertyName} must be between 3 and 100 characters.")
            .Matches(@"^[A-Za-z\s]+$")
                .WithMessage("{PropertyName} should contain only letters and spaces.");
    }

    public static IRuleBuilderOptions<T, string> ApplySortFieldValidation<T>(
    this IRuleBuilder<T, string> ruleBuilder,
    string[] validFields)
    {
        return ruleBuilder
            .Must(sortField => validFields.Contains(sortField.ToLower()))
            .WithMessage("The value '{PropertyValue}' is not valid for {PropertyName}. Valid values are: " + string.Join(", ", validFields));
    }

    private static readonly string[] sourceArray = ["asc", "desc"];

    public static IRuleBuilderOptions<T, string> ApplySortDirectionValidation<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(sortDirection => sourceArray.Contains(sortDirection.ToLower()))
            .WithMessage("The value '{PropertyValue}' is not valid for {PropertyName}. It must be either 'asc' or 'desc'.");
    }
}
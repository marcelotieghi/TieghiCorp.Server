using FluentValidation;

namespace TieghiCorp.API.Filters;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext ctx, EndpointFilterDelegate next)
    {
        var validator = ctx.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator == null)
            return await next(ctx);

        var entity = ctx.Arguments.OfType<T>().FirstOrDefault();
        if (entity == null)
            return Results.Problem("Error Not Found");

        var results = await validator.ValidateAsync(entity);
        return results.IsValid
            ? await next(ctx)
            : Results.ValidationProblem(results.ToDictionary());
    }
}
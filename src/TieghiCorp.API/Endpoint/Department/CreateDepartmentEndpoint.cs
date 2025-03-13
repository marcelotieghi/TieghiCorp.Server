using MediatR;
using Microsoft.AspNetCore.Mvc;
using TieghiCorp.API.Filters;
using TieghiCorp.UseCases.Department.Create;

namespace TieghiCorp.API.Endpoint.Department;

public abstract class CreateDepartmentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapPost("/", HandleAsync)
            .WithName("Department: Create")
            .WithSummary("Create a new department!")
            .AddEndpointFilter<ValidationFilter<CreateDepartmentRequest>>()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);

    public static async Task<IResult> HandleAsync(
       ISender sender,
       [FromBody] CreateDepartmentRequest request,
       CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(request, cancellationToken);
            return result.IsSuccess
                ? TypedResults.Created()
                : TypedResults.Problem(
                    title: result.Error.Title,
                    statusCode: (int)result.Error.Code,
                    detail: result.Error.Message);
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(
                title: "An unexpected error occurred.",
                statusCode: StatusCodes.Status500InternalServerError,
                detail: ex.Message);
        }
    }
}
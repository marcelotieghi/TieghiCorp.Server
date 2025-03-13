using MediatR;
using TieghiCorp.API.Filters;
using TieghiCorp.UseCases.Department.GetById;

namespace TieghiCorp.API.Endpoint.Department;

public abstract class GetDepartmentByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapGet("/{id:int}", HandlerAsync)
            .WithName("Department: GetById")
            .WithSummary("Get a exist department by Id!")
            .AddEndpointFilter<ValidationFilter<GetDepartmentByIdRequest>>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandlerAsync(
        ISender sender,
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            if (id <= 0)
                return TypedResults.BadRequest("The ID must be a positive integer.");

            var result = await sender.Send(new GetDepartmentByIdRequest(id), cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result.Data)
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
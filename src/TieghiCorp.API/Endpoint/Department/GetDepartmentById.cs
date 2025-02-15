using MediatR;
using TieghiCorp.UseCases.Department.GetById;

namespace TieghiCorp.API.Endpoint.Department;

public abstract class GetDepartmentById : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
       => endpoint
           .MapGet("/{id:int}", HandlerAsync)
           .WithName("Departments: GetById")
           .WithSummary("Get a exist department by Id!")
           .Produces(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status400BadRequest)
           .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandlerAsync(
        ISender sender,
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(new GetDepartmentByIdRequest(id), cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.Problem(
                    detail: result.Error.Message,
                    title: "An unexpected error occurred.",
                    statusCode: StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(
                detail: ex.Message,
                title: "An unexpected error occurred.",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}

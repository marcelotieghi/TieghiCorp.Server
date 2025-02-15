using MediatR;
using TieghiCorp.UseCases.Department.Delete;

namespace TieghiCorp.API.Endpoint.Department;

public abstract class DeleteDepartment : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
       => endpoint
           .MapDelete("/{id:int}", HandleAsync)
           .WithName("Departments: Delete")
           .WithSummary("Delete a exist department!")
           .Produces(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status400BadRequest)
           .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandleAsync(
        ISender sender,
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(new DeleteDepartmentRequest(id), cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok("Department delete with success")
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

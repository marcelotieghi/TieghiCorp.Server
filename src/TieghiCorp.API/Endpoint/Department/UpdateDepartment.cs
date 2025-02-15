using MediatR;
using Microsoft.AspNetCore.Mvc;
using TieghiCorp.UseCases.Department.Update;

namespace TieghiCorp.API.Endpoint.Department;

public abstract class UpdateDepartment : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
       => endpoint
           .MapPut("/{id:int}", HandlerAsync)
           .WithName("Departments: Update")
           .WithSummary("Update a exist department!")
           .Produces(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status400BadRequest)
           .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandlerAsync(
        ISender sender,
        int id,
        [FromBody] UpdateDepartmentRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (id != request.Id)
            {
                return TypedResults.BadRequest("Id not matched");
            }

            var result = await sender.Send(request, cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok("Department updated with success!")
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

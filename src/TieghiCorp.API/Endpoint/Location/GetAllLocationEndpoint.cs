using MediatR;
using Microsoft.AspNetCore.Mvc;
using TieghiCorp.UseCases.Location.GetAll;

namespace TieghiCorp.API.Endpoint.Location;

public abstract class GetAllLocationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapGet("/", HandleAsync)
            .WithName("Location: List")
            .WithSummary("Get a list of locations!")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandleAsync(
         ISender sender,
         [FromQuery] int pageNumber = 1,
         [FromQuery] int pageSize = 25,
         [FromQuery] string searchTerm = "",
         [FromQuery] string sortField = "id",
         [FromQuery] string sortDirection = "asc",
         CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new GetAllLocationRequest(
                pageNumber,
                pageSize,
                searchTerm,
                sortField,
                sortDirection
            );

            var result = await sender.Send(request, cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result)
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
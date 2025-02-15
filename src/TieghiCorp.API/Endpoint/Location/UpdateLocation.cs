using MediatR;
using Microsoft.AspNetCore.Mvc;
using TieghiCorp.UseCases.Location.Update;

namespace TieghiCorp.API.Endpoint.Location;

public abstract class UpdateLocation : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapPut("/{id:int}", HandleAsync)
            .WithName("Locations: Update")
            .WithSummary("Update a exist location!")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandleAsync(
        ISender sender,
        int id,
        [FromBody] UpdateLocationRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id != id)
                return TypedResults.BadRequest();

            var result = await sender.Send(request, cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok("Location updated with success!")
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
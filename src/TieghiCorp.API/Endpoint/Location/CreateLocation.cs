using MediatR;
using Microsoft.AspNetCore.Mvc;
using TieghiCorp.UseCases.Location.Create;

namespace TieghiCorp.API.Endpoint.Location;

public abstract class CreateLocation : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapPost("/", HandleAsync)
            .WithName("Locations: Create")
            .WithSummary("Create a new location!")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

    public static async Task<IResult> HandleAsync(
        ISender sender,
        [FromBody] CreateLocationRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(request, cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok("Location created with success!")
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
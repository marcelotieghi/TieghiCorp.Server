using MediatR;
using TieghiCorp.UseCases.Location.Delete;

namespace TieghiCorp.API.Endpoint.Location;

public abstract class DeleteLocation : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapDelete("/{id:int}", HandleAsync)
            .WithName("Locations: SoftDelete")
            .WithSummary("Delete a exist location!")
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
            var result = await sender.Send(new DeleteLocationRequest(id), cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok("Location deleted with success!")
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
using MediatR;
using TieghiCorp.UseCases.Location.Delete;

namespace TieghiCorp.API.Endpoint.Location;

public abstract class DeleteLocationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapDelete("/{id:int}", HandleAsync)
            .WithName("Location: Delete")
            .WithSummary("Delete a exist location!")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
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
                ? TypedResults.Ok()
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
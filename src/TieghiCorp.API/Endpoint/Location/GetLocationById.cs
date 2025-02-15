using MediatR;
using TieghiCorp.UseCases.Location.GetById;

namespace TieghiCorp.API.Endpoint.Location;

public abstract class GetLocationById : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapGet("/{id:int}", HandleAsync)
            .WithName("Locations: GetById")
            .WithSummary("Get a exist location by Id!")
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
            if (id <= 0)
                return TypedResults.BadRequest("The ID must be a positive integer.");

            var result = await sender.Send(new GetLocationByIdRequest(id), cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result.Data)
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
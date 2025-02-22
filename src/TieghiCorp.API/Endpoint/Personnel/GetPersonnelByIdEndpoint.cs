using MediatR;
using TieghiCorp.UseCases.Personnel.GetById;

namespace TieghiCorp.API.Endpoint.Personnel;

public abstract class GetPersonnelByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapGet("/{id:int}", HandlerAsync)
            .WithName("Personnel: GetById")
            .WithSummary("Get a exist personnel by Id!")
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

            var result = await sender.Send(new GetPersonnelByIdRequest(id), cancellationToken);
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
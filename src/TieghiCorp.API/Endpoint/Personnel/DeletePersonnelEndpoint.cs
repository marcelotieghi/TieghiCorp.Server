using MediatR;
using TieghiCorp.API.Filters;
using TieghiCorp.UseCases.Personnel.Delete;

namespace TieghiCorp.API.Endpoint.Personnel;

public abstract class DeletePersonnelEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapDelete("/{id:int}", HandleAsync)
            .WithName("Personnel: Delete")
            .WithSummary("Delete a exist personnel!")
            .AddEndpointFilter<ValidationFilter<DeletePersonnelRequest>>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandleAsync(
        ISender sender,
        [AsParameters] DeletePersonnelRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(request, cancellationToken);
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
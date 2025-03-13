using MediatR;
using TieghiCorp.API.Filters;
using TieghiCorp.UseCases.Location.GetAll;

namespace TieghiCorp.API.Endpoint.Location;

public abstract class GetAllLocationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapGet("/", HandleAsync)
            .WithName("Location: List")
            .WithSummary("Get a list of locations!")
            .AddEndpointFilter<ValidationFilter<GetAllLocationRequest>>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandleAsync(
        ISender sender,
        [AsParameters] GetAllLocationRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
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
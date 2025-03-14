using MediatR;
using TieghiCorp.API.Filters;
using TieghiCorp.UseCases.Personnel.GetAll;

namespace TieghiCorp.API.Endpoint.Personnell;

public class GetAllPersonnelEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapGet("/", HandleAsync)
            .WithName("Personnel: List")
            .WithSummary("Get a list of personnel!")
            .AddEndpointFilter<ValidationFilter<GetAllPersonnelRequest>>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandleAsync(
        ISender sender,
        [AsParameters] GetAllPersonnelRequest request,
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
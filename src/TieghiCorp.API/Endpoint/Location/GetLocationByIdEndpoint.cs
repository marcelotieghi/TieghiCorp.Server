using MediatR;
using TieghiCorp.API.Filters;
using TieghiCorp.UseCases.Location.GetById;

namespace TieghiCorp.API.Endpoint.Location;

public abstract class GetLocationByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapGet("/{id:int}", HandleAsync)
            .WithName("Location: GetById")
            .WithSummary("Get a exist location by Id!")
            .AddEndpointFilter<ValidationFilter<GetLocationByIdRequest>>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandleAsync(
        ISender sender,
        [AsParameters] GetLocationByIdRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(request, cancellationToken);
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
using MediatR;
using TieghiCorp.API.Filters;
using TieghiCorp.UseCases.Personnel.GetById;

namespace TieghiCorp.API.Endpoint.Personnel;

public abstract class GetPersonnelByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapGet("/{id:int}", HandlerAsync)
            .WithName("Personnel: GetById")
            .WithSummary("Get a exist personnel by Id!")
            .AddEndpointFilter<ValidationFilter<GetPersonnelByIdRequest>>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandlerAsync(
        ISender sender,
        [AsParameters] GetPersonnelByIdRequest request,
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
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TieghiCorp.API.Filters;
using TieghiCorp.UseCases.Personnel.Update;

namespace TieghiCorp.API.Endpoint.Personnel;

public abstract class UpdatePersonnelEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapPut("/{id:int}", HandlerAsync)
            .WithName("Personnel: Update")
            .WithSummary("Update a exist personnel!")
            .AddEndpointFilter<ValidationFilter<UpdatePersonnelRequest>>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandlerAsync(
        ISender sender,
        int id,
        [FromBody] UpdatePersonnelRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id != id)
                return TypedResults.BadRequest();

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
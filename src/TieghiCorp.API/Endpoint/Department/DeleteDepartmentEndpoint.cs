﻿using MediatR;
using TieghiCorp.API.Filters;
using TieghiCorp.UseCases.Department.Delete;

namespace TieghiCorp.API.Endpoint.Department;

public abstract class DeleteDepartmentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder endpoint)
        => endpoint
            .MapDelete("/{id:int}", HandleAsync)
            .WithName("Department: Delete")
            .WithSummary("Delete a exist department!")
            .AddEndpointFilter<ValidationFilter<DeleteDepartmentRequest>>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);

    private static async Task<IResult> HandleAsync(
        ISender sender,
        [AsParameters] DeleteDepartmentRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(request, cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok("Department delete with success")
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
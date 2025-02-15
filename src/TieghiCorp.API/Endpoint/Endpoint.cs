using TieghiCorp.API.Endpoint.Department;
using TieghiCorp.API.Endpoint.Location;

namespace TieghiCorp.API.Endpoint;

public static class Endpoint
{

    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints
            .MapGroup("/")
            .WithTags("0 - Health Check")
            .MapGet("/", () => new { message = "OK" });

        endpoints
            .MapGroup("v1/locations")
            .WithTags("Locations")
            .MapEndpoint<CreateLocation>()
            .MapEndpoint<UpdateLocation>()
            .MapEndpoint<DeleteLocation>()
            .MapEndpoint<GetLocationById>()
            .MapEndpoint<GetAllLocationEndpoint>();

        endpoints
            .MapGroup("v1/departments")
            .WithTags("Department")
            .MapEndpoint<CreateDepartment>()
            .MapEndpoint<UpdateDepartment>()
            .MapEndpoint<DeleteDepartment>()
            .MapEndpoint<GetDepartmentById>()
            .MapEndpoint<GetAllDepartment>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder endpoint)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(endpoint);
        return endpoint;
    }
}
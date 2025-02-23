using TieghiCorp.API.Endpoint.Department;
using TieghiCorp.API.Endpoint.Location;
using TieghiCorp.API.Endpoint.Personnel;
using TieghiCorp.API.Endpoint.Personnell;

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
            .MapEndpoint<CreateLocationEndpoint>()
            .MapEndpoint<UpdateLocationEndpoint>()
            .MapEndpoint<DeleteLocationEndpoint>()
            .MapEndpoint<GetLocationByIdEndpoint>()
            .MapEndpoint<GetAllLocationEndpoint>();

        endpoints
            .MapGroup("v1/departments")
            .WithTags("Department")
            .MapEndpoint<CreateDepartmentEndpoint>()
            .MapEndpoint<UpdateDepartmentEndpoint>()
            .MapEndpoint<DeleteDepartmentEndpoint>()
            .MapEndpoint<GetDepartmentByIdEndpoint>()
            .MapEndpoint<GetAllDepartmentEndpoint>();

        endpoints
            .MapGroup("v1/personnel")
            .WithTags("Personnel")
            .MapEndpoint<CreatePersonnelEndpoint>()
            .MapEndpoint<UpdatePersonnelEndpoint>()
            .MapEndpoint<DeletePersonnelEndpoint>()
            .MapEndpoint<GetPersonnelByIdEndpoint>()
            .MapEndpoint<GetAllPersonnelEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder endpoint)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(endpoint);
        return endpoint;
    }
}
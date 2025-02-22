namespace TieghiCorp.API.Endpoint;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder endpoint);
}
using System.Net;

namespace TieghiCorp.Core.Response;

public sealed record HttpError(HttpStatusCode Code, string Message)
{
    public static HttpError None
        => new(0, string.Empty);

    public static HttpError NotFound(string entityName, object propertyValue)
        => new(HttpStatusCode.NotFound, $"{entityName} with Id '{propertyValue}' not found!");

    public static HttpError Conflict(string entityName, string propertyName, object propertyValue)
        => new(HttpStatusCode.Conflict, $"The {entityName} with {propertyName} '{propertyValue}' alredy exist!");
}
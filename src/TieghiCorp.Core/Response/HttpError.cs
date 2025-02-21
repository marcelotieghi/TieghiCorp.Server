using System.Net;

namespace TieghiCorp.Core.Response;

public sealed record HttpError(HttpStatusCode Code, string Title, string Message)
{
    public static HttpError None
       => new(
           Code: 0,
           Title: string.Empty,
           Message: string.Empty);

    public static HttpError NotFound(string entityName, object propertyValue)
        => new(
            Code: HttpStatusCode.NotFound,
            Title: "Entity Not Found",
            Message: $"{entityName} with Id '{propertyValue}' not found!");

    public static HttpError Conflict(string entityName, string propertyName, object propertyValue)
        => new(
            Code: HttpStatusCode.Conflict,
            Title: "Entity Already Exists",
            Message: $"The {entityName} with {propertyName} '{propertyValue}' already exists!");

    public static HttpError DependencyConflict(string entityName, string dependentEntityName)
       => new(
           Code: HttpStatusCode.Conflict,
           Title: "Entity Cannot Be Deleted Due to Dependencies",
           Message: $"The {entityName} cannot be deleted because it has dependent {dependentEntityName} entities.");
}
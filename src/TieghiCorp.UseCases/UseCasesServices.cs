using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TieghiCorp.UseCases;

public static class UseCasesServices
{
    public static void AddUseCasesServices(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
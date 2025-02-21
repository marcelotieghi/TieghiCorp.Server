using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TieghiCorp.Infra.Data.Context;

namespace TieghiCorp.Infra;

public static class InfraServices
{
    public static void AddInfraServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(
            opt => opt.UseSqlServer(config.GetConnectionString("DefaultConn")));
    }
}
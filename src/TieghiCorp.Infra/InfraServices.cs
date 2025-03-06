using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TieghiCorp.Core.Entities;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Infra.Data.Context;
using TieghiCorp.Infra.Repos;

namespace TieghiCorp.Infra;

public static class InfraServices
{
    public static void AddInfraServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        //services.AddDbContext<AppDbContext>(
        //    opt => opt.UseSqlServer(config.GetConnectionString("DefaultConn")));

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                config.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
            ));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services
            .AddScoped<ICommandRepos<Location>, CommandRepos<Location>>()
            .AddScoped<IQueryRepos<Location>, QueryRepos<Location>>();

        services
            .AddScoped<ICommandRepos<Department>, CommandRepos<Department>>()
            .AddScoped<IQueryRepos<Department>, QueryRepos<Department>>();

        services
            .AddScoped<ICommandRepos<Personnel>, CommandRepos<Personnel>>()
            .AddScoped<IQueryRepos<Personnel>, QueryRepos<Personnel>>();
    }
}
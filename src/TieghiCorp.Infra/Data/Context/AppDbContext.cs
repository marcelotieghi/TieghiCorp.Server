using Microsoft.EntityFrameworkCore;
using TieghiCorp.Core.Entities;

namespace TieghiCorp.Infra.Data.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext> opt) : DbContext(opt)
{
    public DbSet<Location> Location => Set<Location>();
    public DbSet<Department> Department => Set<Department>();
    public DbSet<Personnel> Personnel => Set<Personnel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
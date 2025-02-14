using TieghiCorp.Core.Entities;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Infra.Data.Context;

namespace TieghiCorp.Infra.Repos;

internal class UnitOfWork(AppDbContext ctx) : IUnitOfWork
{
    private readonly AppDbContext _ctx = ctx;

    private bool _disposed;

    private ICommandRepos<Location>? _locationCommand;
    private ICommandRepos<Department>? _departmentCommand;
    private ICommandRepos<Personnel>? _personnelCommand;

    public ICommandRepos<Location> LocationCommand
        => _locationCommand ??= new CommandRepos<Location>(_ctx);
    public ICommandRepos<Department> DepartmentCommand
        => _departmentCommand ??= new CommandRepos<Department>(_ctx);
    public ICommandRepos<Personnel> PersonnelCommand
        => _personnelCommand ??= new CommandRepos<Personnel>(_ctx);

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
        => await _ctx.SaveChangesAsync(token);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _ctx.Dispose();
        }
        _disposed = true;
    }
}
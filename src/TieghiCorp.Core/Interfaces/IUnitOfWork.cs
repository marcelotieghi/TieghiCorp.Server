using TieghiCorp.Core.Entities;

namespace TieghiCorp.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICommandRepos<Location> LocationCommand { get; }
    ICommandRepos<Department> DepartmentCommand { get; }
    ICommandRepos<Personnel> PersonnelCommand { get; }

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
using TieghiCorp.Core.Abstract;

namespace TieghiCorp.Core.Interfaces;

public interface ICommandRepos<in T> where T : BaseEntity
{
    Task CreateAsync(T entity, CancellationToken token);
    Task UpdateAsync(T entity, CancellationToken token);
    Task DeleteAsync(int id, CancellationToken token);
}
using System.Linq.Expressions;
using TieghiCorp.Core.Abstract;

namespace TieghiCorp.Core.Interfaces;

public interface IQueryRepos<T> where T : BaseEntity
{
    Task<T> GetByKeyAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
    Task<bool> ExistByKeyAsync(Expression<Func<T, bool>> expression, CancellationToken token = default);
    IQueryable<T> GetAll();
}
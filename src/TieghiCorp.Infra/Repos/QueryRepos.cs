using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TieghiCorp.Core.Abstract;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Infra.Data.Context;

namespace TieghiCorp.Infra.Repos;

internal sealed class QueryRepos<T>(AppDbContext ctx) : IQueryRepos<T> where T : BaseEntity
{
    private readonly DbSet<T> _ctx = ctx.Set<T>();

    public async Task<bool> ExistByKeyAsync(Expression<Func<T, bool>> expression, CancellationToken token)
        => await _ctx.AnyAsync(expression, token);

    public async Task<T> GetByKeyAsync(Expression<Func<T, bool>> predicate, CancellationToken token)
        => await _ctx.FirstOrDefaultAsync(predicate, token).ConfigureAwait(false) ?? null!;

    public IQueryable<T> GetAll()
        => _ctx.AsQueryable<T>();
}
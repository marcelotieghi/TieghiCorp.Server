using Microsoft.EntityFrameworkCore;
using TieghiCorp.Core.Abstract;
using TieghiCorp.Core.Interfaces;
using TieghiCorp.Infra.Data.Context;

namespace TieghiCorp.Infra.Repos;

internal sealed class CommandRepos<T>(AppDbContext ctx) : ICommandRepos<T> where T : BaseEntity
{
    private readonly DbSet<T> _ctx = ctx.Set<T>();

    public async Task CreateAsync(T entity, CancellationToken token = default)
        => await _ctx.AddAsync(entity, token);

    public async Task DeleteAsync(int id, CancellationToken token = default)
        => await _ctx.Where(e => e.Id == id).ExecuteDeleteAsync(token);

    public async Task UpdateAsync(T entity, CancellationToken token = default)
        => await Task.Run(() => _ctx.Update(entity), token);
}

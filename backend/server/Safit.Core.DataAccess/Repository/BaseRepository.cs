using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
{
    protected DatabaseContext context;

    public BaseRepository(DatabaseContext context)
    {
        this.context = context; 
    }

    public async Task<IQueryable<T>> FindAll(CancellationToken ct = default)
    {
        return await Task.Run(() => context.Set<T>().AsNoTracking());
    }
    public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken ct = default)
    {
        return await Task.Run(() => context.Set<T>().Where(expression).AsNoTracking());
    }
    public virtual async Task Create(T entity, CancellationToken ct = default)
    {
        await context.Set<T>().AddAsync(entity, ct);
    }
    public virtual async Task Delete(T entity, CancellationToken ct = default)
    {
        await Task.Run(() => context.Set<T>().Remove(entity), ct);
    }
    public virtual async Task Update(T entity, CancellationToken ct = default)
    {
        await Task.Run(() => context.Set<T>().Update(entity), ct);
    }

    
}
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
{
    protected DatabaseContext context;

    public BaseRepository(DatabaseContext context)
    {
        this.context = context; 
    }

    public async Task<IQueryable<T>> FindAll()
    {
        return await Task.Run(() => context.Set<T>().AsNoTracking());
    }
    public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return await Task.Run(() => context.Set<T>().Where(expression).AsNoTracking());
    }
    public virtual async Task Create(T entity)
    {
        await context.Set<T>().AddAsync(entity);
    }
    public virtual async Task Delete(T entity)
    {
        await Task.Run(() => context.Set<T>().Remove(entity));
    }
    public virtual async Task Update(T entity)
    {
        await Task.Run(() => context.Set<T>().Update(entity));
    }

    
}
using System.Linq.Expressions;

namespace Safit.Core.Domain.Repository;

public interface IBaseRepository<T>
{
    Task<IQueryable<T>> FindAll(CancellationToken ct = default);
    Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken ct = default);
    Task Create(T entity, CancellationToken ct = default);
    Task Update(T entity, CancellationToken ct = default);
    Task Delete(T entity, CancellationToken ct = default);
}
using System.Linq.Expressions;

namespace Safit.Core.DataAccess;

public interface IBaseRepository<T>
{
    Task<IQueryable<T>> FindAll();
    Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression);
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}
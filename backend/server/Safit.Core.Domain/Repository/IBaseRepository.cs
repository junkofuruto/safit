using System.Linq.Expressions;
using Safit.Core.Domain.Repository.Exceptions;

namespace Safit.Core.Domain.Repository;

public interface IBaseRepository<T>
{
    /// <summary>
    /// Method allows to execute raw SQL query
    /// </summary>
    /// <param name="query">SQL query to execute</param>
    /// <param name="ct">Cancellation Token that will interrupt execution of query</param>
    /// <param name="paremeters">SQL query parameters</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="ObjectDisposedException"></exception>
    /// <returns>Result of SQL query execution</returns>
    public Task<IQueryable<T>> ExecuteQuery(string query, CancellationToken ct = default, params object[] paremeters);

    /// <summary>
    /// Method basically runs "SELECT * FROM entity"
    /// </summary>
    /// <param name="ct">Cancellation Token that will interrupt execution of query</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="ObjectDisposedException"></exception>
    /// <returns>Whole set of entity in database</returns>
    public Task<IQueryable<T>> FindAll(CancellationToken ct = default);

    /// <summary>
    /// Method basically runs "SELECT * FROM entity WHERE expression"
    /// </summary>
    /// <param name="expression">Lambda expression that translates to SQL query</param>
    /// <param name="ct">Cancellation Token that will interrupt execution of query</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="TaskCanceledException"></exception>
    /// <returns>Set of entity in database that corresponds expression</returns>
    public Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken ct = default);

    /// <summary>
    /// Method basically runs "INSERT INTO entity VALUES (...)"
    /// </summary>
    /// <param name="entity">Entity that will be inserted</param>
    /// <param name="ct">Cancellation Token that will interrupt execution of query</param>
    /// <exception cref="OperationCanceledException"></exception>
    /// <exception cref="EntityNullException"></exception>
    public Task Create(T entity, CancellationToken ct = default);

    /// <summary>
    /// Method basically runs "DELETE FROM entity WHERE expression"
    /// </summary>
    /// <param name="entity">Entity that will be deleted, key values have a role of </param>
    /// <param name="ct">Cancellation Token that will interrupt execution of query</param>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="ObjectDisposedException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="EntityNullException"></exception>
    public Task Delete(T entity, CancellationToken ct = default);

    /// <summary>
    /// Method basically runs "UPDATE entity SET ... WHERE [entity keys comparison]"
    /// </summary>
    /// <param name="entity">Entity that will be updated, key values have a role of </param>
    /// <param name="ct">Cancellation Token that will interrupt execution of query</param>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="ObjectDisposedException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="EntityNullException"></exception>
    public Task Update(T entity, CancellationToken ct = default);
}
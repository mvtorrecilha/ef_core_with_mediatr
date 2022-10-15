using System.Linq.Expressions;

namespace Library.Domain.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllWithIncludes(params Expression<Func<T, Object>>[] includes);
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
    Task<T> FindByAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}

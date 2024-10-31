using System.Linq.Expressions;

namespace BlazeM4BManager.Domain.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

    Task<T> AddAsync(T entity);

    Task AddRangeAsync(List<T> entities);

    Task<int> ExecuteDeleteAsync(Expression<Func<T, bool>> predicate);

    Task<T> UpdateAsync(T entity);

    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    IQueryable<T> GetQuery();
}

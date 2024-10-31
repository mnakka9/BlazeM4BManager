using System.Linq.Expressions;
using BlazeM4BManager.Domain.Models;
using BlazeM4BManager.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazeM4BManager.Domain.Repositories;

public class Repository<T>(BlazeAppContext context) : IRepository<T> where T : class
{
    private DbSet<T> DbSet => context.Set<T>();

    public async Task<T> AddAsync(T entity)
    {
        DbSet.Add(entity);

        return await new ValueTask<T>(entity);
    }

    public async Task AddRangeAsync(List<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await DbSet.FindAsync(id);

        if (entity is null)
        {
            return false;
        }

        DbSet.Remove(entity);

        return true;
    }

    public async Task<int> ExecuteDeleteAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.Where(predicate).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.Where(predicate).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        DbSet.Update(entity);

        return await new ValueTask<T>(entity);
    }

    public IQueryable<T> GetQuery()
    {
        return DbSet.AsQueryable();
    }
}

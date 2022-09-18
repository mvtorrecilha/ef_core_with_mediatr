using Library.Core.Interfaces.Repositories;
using Library.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly LibraryContext _context;

    public BaseRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllWithIncludes(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> FindByAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }
}
using Microsoft.EntityFrameworkCore;
using back_end.Repository.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using back_end.Data;
using System.Linq;
using System;

namespace back_end.Repository.Implement;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly FSContext _db;
    internal DbSet<T> dbSet;

    public Repository(FSContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null,
                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                string includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (string includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null,
                                string includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T> Get(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task Add(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public async Task Update(T entity)
    {
        await Task.Run(() => dbSet.Update(entity));
    }

    public async Task Remove(int id)
    {
        T entity = await dbSet.FindAsync(id);
        await Remove(entity);
    }

    public async Task Remove(T entity)
    {
        await Task.Run(() => dbSet.Remove(entity));
    }

    public async Task RemoveRange(IEnumerable<T> entity)
    {
        await Task.Run(() => dbSet.RemoveRange(entity));
    }
}

using Courseproject.Common.Interface;
using Courseproject.Common.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Courseproject.Infrastructure;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private ApplicationDbContext applicationDbContext { get; }
    private DbSet<T> dbSet { get; }

    public GenericRepository(ApplicationDbContext db)
    {
        applicationDbContext = db;
        dbSet = db.Set<T>();   
    }

    public void Delete(T entity)
    {
        if(applicationDbContext.Entry(entity).State == EntityState.Detached)
            dbSet.Attach(entity);

        dbSet.Remove(entity);
    }

    public async Task<List<T>> GetAsync(int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;

        foreach(var include in includes)
            query = query.Include(include);

        if (skip != null)
            query = query.Skip(skip.Value);

        if(take != null)
            query = query.Take(take.Value); 

        return await query.ToListAsync();
    }

    public async Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>>[] filters, int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
       
        foreach(var filter in filters)
            query = query.Where(filter);

        foreach(var include in includes)
            query= query.Include(include); 
        
        if(skip != null)
            query = query.Skip(skip.Value);

        if(take != null)
            query = query.Take(take.Value);

        return await query.ToListAsync();
    }

    public async Task<T?> getByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;

        query = query.Where(entity => entity.Id == id);

        foreach (var include in includes)
            query = query.Include(include);

        return await query.SingleOrDefaultAsync();
    }

    public async Task<int> InsertAsync(T entity)
    {
       await dbSet.AddAsync(entity);
       return entity.Id;
    }

    public async Task SaveChangesAsync()
    {
        await applicationDbContext.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        dbSet.Attach(entity);
        applicationDbContext.Entry(entity).State = EntityState.Modified;
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;

namespace MauiAccelerator.Core.Database.Repositories;

public class Repository<T>(SQLiteAsyncConnection db) : IRepository<T>
    where T : class, new()
{
    public async Task<List<T>> GetItemsAsync() => await db.Table<T>().ToListAsync();

    public async Task<List<T>> GetItemsAsync<TValue>(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, TValue>>? orderBy = null)
    {
        var query = db.Table<T>();
        
        if (predicate != null)
            query = query.Where(predicate);
        
        if (orderBy != null)
            query = query.OrderBy<TValue>(orderBy);

        return await query.ToListAsync();
    }

    public async Task<T> GetItemAsync(int id) => 
        await db.FindAsync<T>(id);

    public async Task<T> GetItemAsync(Expression<Func<T, bool>> predicate) =>
        await db.FindAsync<T>(predicate);

    public virtual Task<int> UpsertItemAsync(T entity)
    {
        return Task.FromResult(-1);
    }

    public async Task<int> DeleteItemAsync(T entity) =>
        await db.DeleteAsync(entity);
}
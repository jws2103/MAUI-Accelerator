using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MauiAccelerator.Core.Database.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetItemsAsync();
    Task<T> GetItemAsync(int id);
    Task<List<T>> GetItemsAsync<TValue>(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, TValue>>? orderBy = null);
    Task<T> GetItemAsync(Expression<Func<T, bool>> predicate);
    Task<int> UpsertItemAsync(T entity);
    Task<int> DeleteItemAsync(T entity);
}
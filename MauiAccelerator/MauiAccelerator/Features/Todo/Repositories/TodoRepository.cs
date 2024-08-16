using System.Threading.Tasks;
using MauiAccelerator.Core.Database.Repositories;
using MauiAccelerator.Features.Todo.Models;
using SQLite;

namespace MauiAccelerator.Features.Todo.Repositories;

public class TodoRepository(SQLiteAsyncConnection db) : Repository<TodoItem>(db)
{
    private readonly SQLiteAsyncConnection _db = db;

    public override async Task<int> UpsertItemAsync(TodoItem entity)
    {
        if (entity.ID != 0)
        {
            return await _db.UpdateAsync(entity);
        }

        return await _db.InsertAsync(entity);
    }
}
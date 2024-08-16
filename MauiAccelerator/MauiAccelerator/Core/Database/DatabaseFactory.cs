using System.IO;
using MauiAccelerator.Core.Database.Repositories;
using MauiAccelerator.Core.Extensions;
using MauiAccelerator.Features.Todo.Models;
using MauiAccelerator.Features.Todo.Repositories;
using Microsoft.Maui.Storage;
using SQLite;

namespace MauiAccelerator.Core.Database;

public static class DatabaseFactory
{
    private const string DatabaseFilename = "TodoSQLite.db3";

    private const SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLiteOpenFlags.SharedCache;

    private static readonly string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    
    public static IRepository<TodoItem> BuildDatabaseRepository()
    {
        var database = new SQLiteAsyncConnection(DatabasePath, Flags);
        database.CreateTableAsync<TodoItem>().Forget();
        return new TodoRepository(database);
    }
}
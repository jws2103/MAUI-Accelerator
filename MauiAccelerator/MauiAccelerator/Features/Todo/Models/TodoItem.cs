using MauiAccelerator.Core.Database;
using MauiAccelerator.Core.Database.Models;
using SQLite;

namespace MauiAccelerator.Features.Todo.Models;

public class TodoItem : IPersistable
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public bool Done { get; set; }
}
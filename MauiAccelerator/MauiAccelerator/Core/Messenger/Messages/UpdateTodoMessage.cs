using CommunityToolkit.Mvvm.Messaging.Messages;
using MauiAccelerator.Features.Todo.Models;

namespace MauiAccelerator.Core.Messenger.Messages;

public enum TodoUserCommandType
{
    Saved,
    Deleted
}

public class UpdateTodoMessage(TodoItem todoItem, TodoUserCommandType userCommandTypeType)
    : ValueChangedMessage<TodoItem>(todoItem), IMessage
{
    public TodoUserCommandType UserCommandTypeType { get; } = userCommandTypeType;
}
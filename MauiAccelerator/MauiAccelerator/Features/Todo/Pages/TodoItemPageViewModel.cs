using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAccelerator.Core.Constants;
using MauiAccelerator.Core.Database.Repositories;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Core.Messenger;
using MauiAccelerator.Core.Messenger.Messages;
using MauiAccelerator.Core.Navigation;
using MauiAccelerator.Features.Common.Pages;
using MauiAccelerator.Features.Todo.Models;
using Microsoft.Maui.Controls;

namespace MauiAccelerator.Features.Todo.Pages;

public partial class TodoItemPageViewModel(
    IRepository<TodoItem> repository,
    INavigationService navigationService,
    IAlertService alertService,
    IMessengerService messengerService)
    : BasePageViewModel(navigationService), IQueryAttributable
{
    [ObservableProperty] private TodoItem? _item;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Item = query[AppConstants.NavParameters.ItemKey] as TodoItem;
    }
    
    [RelayCommand]
    private async Task SaveTodo()
    {
        if (string.IsNullOrWhiteSpace(Item?.Name))
        {
            await alertService.ShowAlertAsync(AppConstants.Alerts.Errors.TodoItemErrorHeading, AppConstants.Alerts.Errors.TodoItemErrorContent);
            return;
        }

        await repository.UpsertItemAsync(Item);
        await NavigationService.GoBackAsync();
        messengerService.Send(new UpdateTodoMessage(Item, TodoUserCommandType.Saved));
    }

    [RelayCommand]
    private async Task DeleteTodo()
    {
        if (Item == null || Item.ID == 0)
        {
            return;
        }

        await repository.DeleteItemAsync(Item);
        await NavigationService.GoBackAsync();
        messengerService.Send(new UpdateTodoMessage(Item, TodoUserCommandType.Deleted));
    }
    
    [RelayCommand]
    private async Task Cancel()
    {
        await NavigationService.GoBackAsync();
    }
}
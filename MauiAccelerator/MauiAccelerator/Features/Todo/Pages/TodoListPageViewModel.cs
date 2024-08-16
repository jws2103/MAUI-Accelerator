using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAccelerator.Core.Constants;
using MauiAccelerator.Core.Database.Repositories;
using MauiAccelerator.Core.Extensions;
using MauiAccelerator.Core.Messenger;
using MauiAccelerator.Core.Messenger.Messages;
using MauiAccelerator.Core.Navigation;
using MauiAccelerator.Features.Common.Pages;
using MauiAccelerator.Features.Todo.Models;

namespace MauiAccelerator.Features.Todo.Pages;

public partial class TodoListPageViewModel(
    IRepository<TodoItem> repository, 
    INavigationService navigationService,
    IMessengerService messengerService)
    : BasePageViewModel(navigationService)
{
    [ObservableProperty] 
    private ObservableCollection<TodoItem>? _items;

    [ObservableProperty] private TodoItem? _selectedTodo;

    public override void OnAppearing()
    {
        base.OnAppearing();
        RegisterMessages();
    }
    
    public override void OnDisappearing()
    {
        UnRegisterMessages();
        base.OnDisappearing();
    }

    public override void OnNavigatedTo()
    {
        base.OnNavigatedTo();
        InitData().Forget();
    }

    [RelayCommand]
    private async Task TodoItemSelectionChanged()
    {
        if (SelectedTodo is null)
        {
            return;
        }
        
        await NavigateToItemPage(SelectedTodo);
        SelectedTodo = null;
    }
    
    [RelayCommand]
    private async Task NavigateToItem()
    {
        await NavigateToItemPage(new TodoItem());
    }
    
    private async Task InitData()
    {
        if (Items != null)
        {
            return;
        }
        
        var items = await repository.GetItemsAsync();
        var todoItemList = new List<TodoItem>();
        todoItemList.AddRange(items);
        Items = new ObservableCollection<TodoItem>(todoItemList);
    }

    private async Task NavigateToItemPage(TodoItem selectedTodo)
    {
        await NavigationService.NavigateToAsync(NavigationRoute.TodoItemPage.ToString(), routeParameters: new Dictionary<string, object>
        {
            [AppConstants.NavParameters.ItemKey] = selectedTodo
        });
    }

    private void RegisterMessages()
    {
        messengerService.Register<UpdateTodoMessage>(this, HandleUpdatedMessage);
    }

    private void UnRegisterMessages()
    {
        messengerService.Unregister<UpdateTodoMessage>(this);
    }
    
    private void HandleUpdatedMessage(object recipient, UpdateTodoMessage message)
    {
        var updatedTodoItem = message?.Value;
        var updateStatus = message?.UserCommandTypeType;
        if (Items == null  || updatedTodoItem == null)
        {
            return;
        }

        var matchedItem = Items.FirstOrDefault(i => i.ID == updatedTodoItem.ID);
        if (matchedItem == null)
        {
            if (updateStatus == TodoUserCommandType.Saved)
            {
                Items.Add(updatedTodoItem);
            }
            return;
        }

        var index = Items.IndexOf(matchedItem);
        switch (updateStatus)
        {
            case TodoUserCommandType.Deleted:
                Items.RemoveAt(index);
                break;
            case TodoUserCommandType.Saved:
                Items[index] = updatedTodoItem;
                break;
        }
    }
}
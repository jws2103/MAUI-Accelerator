using CommunityToolkit.Mvvm.Messaging;
using FluentAssertions;
using MauiAccelerator.Core.Database.Repositories;
using MauiAccelerator.Core.Messenger;
using MauiAccelerator.Core.Messenger.Messages;
using MauiAccelerator.Core.Navigation;
using MauiAccelerator.Features.Todo.Models;
using MauiAccelerator.Features.Todo.Pages;
using Moq;

namespace MauiAccelerator.UnitTests.Features.Todo;

public class TodoListPageViewModelTests : UnitTestBase<TodoListPageViewModel>
{
    [Fact]
    public void OnAppearing_ShouldRegisterMessage_WhenInvoked()
    {
        // Arrange
        var mockedMessengerSvc = Mocker.GetMock<IMessengerService>();
        
        // Act
        Sut.OnAppearing();
        
        // Assert
        mockedMessengerSvc.Verify(m => m.Register(It.IsAny<object>(), It.IsAny<MessageHandler<object, UpdateTodoMessage>>()), Times.Once);
    }
    
    [Fact]
    public void OnDisappearing_ShouldUnregisterMessage_WhenInvoked()
    {
        // Arrange
        var mockedMessengerSvc = Mocker.GetMock<IMessengerService>();
        
        // Act
        Sut.OnDisappearing();
        
        // Assert
        mockedMessengerSvc.Verify(m => m.Unregister<UpdateTodoMessage>(It.IsAny<object>()), Times.Once);
    }
    
    [Fact]
    public void OnNavigatedTo_ShouldSetItems_WhenInvoked()
    {
        // Arrange
        var mockedDb = Mocker.GetMock<IRepository<TodoItem>>();
        mockedDb.Setup(d => d.GetItemsAsync()).ReturnsAsync(new List<TodoItem>());
        
        // Act
        Sut.OnNavigatedTo();
        
        // Assert
        mockedDb.Verify(d => d.GetItemsAsync(), Times.Once);
        Sut.Items.Should().NotBeNull();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task TodoItemSelectionChangedCommand_ShouldNavigateToItemPage_WhenSelectedTodoIsNotNull(
        bool hasSelectedTodo)
    {
        // Arrange
        var mockedNavSvc = Mocker.GetMock<INavigationService>();
        Sut.SelectedTodo = hasSelectedTodo ? new TodoItem() : null;

        // Act
        await Sut.TodoItemSelectionChangedCommand.ExecuteAsync(null);

        // Assert
        if (hasSelectedTodo)
        {
            mockedNavSvc.Verify(n => n.NavigateToAsync(NavigationRoute.TodoItemPage.ToString(), false, It.IsAny<Dictionary<string, object>>()), Times.Once);
        }
        else
        {
            mockedNavSvc.Verify(n => n.NavigateToAsync(NavigationRoute.TodoItemPage.ToString(), false, It.IsAny<Dictionary<string, object>>()), Times.Never);
        }
    }

    [Fact]
    public async Task NavigateToItemCommand_ShouldNavigateToItemPage_WhenInvoked()
    {
        // Arrange
        var mockedNavSvc = Mocker.GetMock<INavigationService>();

        // Act
        await Sut.NavigateToItemCommand.ExecuteAsync(null);

        // Assert
        mockedNavSvc.Verify(n => n.NavigateToAsync(NavigationRoute.TodoItemPage.ToString(), false, It.IsAny<Dictionary<string, object>>()), Times.Once);
    }
}
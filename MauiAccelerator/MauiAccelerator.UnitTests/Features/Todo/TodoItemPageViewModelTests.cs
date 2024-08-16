using MauiAccelerator.Core.Constants;
using MauiAccelerator.Core.Database.Repositories;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Core.Messenger;
using MauiAccelerator.Core.Messenger.Messages;
using MauiAccelerator.Core.Navigation;
using MauiAccelerator.Features.Todo.Models;
using MauiAccelerator.Features.Todo.Pages;
using Moq;

namespace MauiAccelerator.UnitTests.Features.Todo;

public class TodoItemPageViewModelTests : UnitTestBase<TodoItemPageViewModel>
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task SaveTodoCommand_ShouldShowAlertToUser_WhenItemIsNullOrEmpty(bool isNull)
    {
        // Arrange
        var mockedAlertSvc = Mocker.GetMock<IAlertService>();
        Sut.Item = !isNull ? new TodoItem() : null;
        
        // Act
        await Sut.SaveTodoCommand.ExecuteAsync(null);
        
        // Assert
        mockedAlertSvc.Verify(a => a.ShowAlertAsync(AppConstants.Alerts.Errors.TodoItemErrorHeading, AppConstants.Alerts.Errors.TodoItemErrorContent, AppConstants.Alerts.OK), Times.Once);
    }
    
    [Fact]
    public async Task SaveTodoCommand_ShouldSaveToDbAndNavigateBack_WhenInvoked()
    {
        // Arrange
        var mockedDb = Mocker.GetMock<IRepository<TodoItem>>();
        var mockedMessengerSvc = Mocker.GetMock<IMessengerService>();
        var mockedNavSvc = Mocker.GetMock<INavigationService>();
        Sut.Item = new TodoItem
        {
            Name = "Test"
        };
        
        // Act
        await Sut.SaveTodoCommand.ExecuteAsync(null);
        
        // Assert
        mockedDb.Verify(d => d.UpsertItemAsync(It.IsAny<TodoItem>()), Times.Once);
        mockedNavSvc.Verify(d => d.GoBackAsync(false), Times.Once);
        mockedMessengerSvc.Verify(m => m.Send(It.IsAny<UpdateTodoMessage>()), Times.Once);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DeleteTodoCommand_ShouldTerminate_WhenItemIsNullOrNotValidId(bool isNull)
    {
        // Arrange
        var mockedDb = Mocker.GetMock<IRepository<TodoItem>>();
        var mockedNavSvc = Mocker.GetMock<INavigationService>();
        Sut.Item = !isNull ? new TodoItem
        {
            ID = 0
        } : null;
        
        // Act
        await Sut.DeleteTodoCommand.ExecuteAsync(null);
        
        // Assert
        mockedDb.Verify(d => d.DeleteItemAsync(It.IsAny<TodoItem>()), Times.Never);
        mockedNavSvc.Verify(d => d.GoBackAsync(false), Times.Never);
    }
    
    [Fact]
    public async Task DeleteTodoCommand_ShouldDeleteFromDbAndNavigateBack_WhenInvoked()
    {
        // Arrange
        var mockedDb = Mocker.GetMock<IRepository<TodoItem>>();
        var mockedMessengerSvc = Mocker.GetMock<IMessengerService>();
        var mockedNavSvc = Mocker.GetMock<INavigationService>();
        Sut.Item = new TodoItem
        {
            ID = 1,
            Name = "Test"
        };
        
        // Act
        await Sut.DeleteTodoCommand.ExecuteAsync(null);
        
        // Assert
        mockedDb.Verify(d => d.DeleteItemAsync(It.IsAny<TodoItem>()), Times.Once);
        mockedNavSvc.Verify(d => d.GoBackAsync(false), Times.Once);
        mockedMessengerSvc.Verify(m => m.Send(It.IsAny<UpdateTodoMessage>()), Times.Once);
    }
    
    [Fact]
    public async Task CancelCommand_ShouldNavigateBack_WhenInvoked()
    {
        // Arrange
        var mockedNavSvc = Mocker.GetMock<INavigationService>();
        
        // Act
        await Sut.CancelCommand.ExecuteAsync(null);
        
        // Assert
        mockedNavSvc.Verify(d => d.GoBackAsync(false), Times.Once);
    }
}
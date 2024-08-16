using MauiAccelerator.Core.Constants;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Core.Navigation;
using MauiAccelerator.Features.Login.Services;
using Moq;

namespace MauiAccelerator.UnitTests;

public class AppShellViewModelTests : UnitTestBase<AppShellViewModel>
{
    [Theory]
    [InlineData(true, true, "TodoListPage")]
    [InlineData(false, true, "LoginPage")]
    [InlineData(true, false, "LoginPage")]
    [InlineData(false, false, "LoginPage")]
    public async Task NavigateToInitialScreen_ShouldNavigateToInitialScreen_WhenInvoked(bool userLoggedIn, bool keepUserLoggedIn, string expectedRoute)
    {
        // Arrange
        var mockedAuthSvc = Mocker.GetMock<IAuthService>();
        var mockedSettingsSvc = Mocker.GetMock<IPreferenceService>();
        var mockedNavSvc = Mocker.GetMock<INavigationService>();
        mockedSettingsSvc.Setup(s => s.UserLoggedIn).Returns(keepUserLoggedIn);
        mockedAuthSvc.Setup(a => a.SignInSilentAsync()).ReturnsAsync(userLoggedIn);
        
        // Act
        await Sut.NavigateToInitialScreen();
        
        // Assert
        if (userLoggedIn && keepUserLoggedIn)
        {
            mockedNavSvc.Verify(n => n.NavigateToAsync( string.Format(AppConstants.Navigation.NavStringFormat, AppConstants.Navigation.RootPagePrefix, expectedRoute), false, null), Times.Once);
        }
    }

    [Fact]
    public async Task LogoutCommand_ShouldNavigateBackToLogin_WhenInvoked()
    {
        // Arrange
        var mockedNavSvc = Mocker.GetMock<INavigationService>();
        
        // Act
        await Sut.LogoutCommand.ExecuteAsync(null);
        
        // Assert
        mockedNavSvc.Verify(n => n.NavigateToAsync(string.Format(AppConstants.Navigation.NavStringFormat, AppConstants.Navigation.RootPagePrefix,
            NavigationRoute.LoginPage.ToString()), false, null), Times.Once);
    }
}
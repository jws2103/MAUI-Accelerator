using FluentAssertions;
using MauiAccelerator.Core.Constants;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Core.Navigation;
using MauiAccelerator.Features.Login.Models;
using MauiAccelerator.Features.Login.Pages;
using MauiAccelerator.Features.Login.Services;
using Moq;

namespace MauiAccelerator.UnitTests.Features.Login;

public class LoginPageViewModelTests :UnitTestBase<LoginPageViewModel>
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TogglePasswordCommand_ShouldInvertIsPassword_WhenInvoked(bool isPassword)
    {
        // Arrange
        Sut.IsPassword = isPassword;
        
        // Act
        Sut.TogglePasswordCommand.Execute(null);
        
        // Assert
        Sut.IsPassword.Should().Be(!isPassword);
    }
    
    [Theory]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(false, false)]
    public async Task LoginCommand_ShouldShowAlert_WhenEmailOrPasswordIsEmpty(bool hasEmail, bool hasPassword)
    {
        // Arrange
        var mockedAlertSvc = Mocker.GetMock<IAlertService>();
        Sut.UserName = hasEmail ? "test" : string.Empty;
        Sut.Password = hasPassword ? "pass" : string.Empty;
        
        // Act
        await Sut.LoginCommand.ExecuteAsync(null);
        
        // Assert
        mockedAlertSvc.Verify(a => a.ShowAlertAsync(AppConstants.Alerts.Errors.ErrorHeading, AppConstants.Alerts.Errors.LoginErrorContent, AppConstants.Alerts.OK), Times.Once);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task LoginCommand_ShouldAlertUser_WhenTokenResponseIsNull(bool hasTokenResponse)
    {
        // Arrange
        var mockedAlertSvc = Mocker.GetMock<IAlertService>();
        var mockedAuthSvc = Mocker.GetMock<IAuthService>();
        var tokenRespponse = hasTokenResponse ? new TokenResponse
        {
            Token = string.Empty,
            RefreshToken = string.Empty
        } : null;
        mockedAuthSvc.Setup(a => a.SignInAsync(It.IsAny<TokenRequest>())).ReturnsAsync(tokenRespponse);
        Sut.UserName = "test";
        Sut.Password = "pass";
        
        // Act
        await Sut.LoginCommand.ExecuteAsync(null);
        
        // Assert
        mockedAlertSvc.Verify(a => a.ShowAlertAsync(AppConstants.Alerts.Errors.ErrorHeading, AppConstants.Alerts.Errors.LoginFailedErrorContent, AppConstants.Alerts.OK), Times.Once);
    }
    
    [Fact]
    public async Task LoginCommand_ShouldNavigateToTodoListAndStoreTokens_WhenLoggedIn()
    {
        // Arrange
        var mockedSecureStorage = Mocker.GetMock<ISecureStorageService>();
        var mockedAuthSvc = Mocker.GetMock<IAuthService>();
        var mockedNavSvc = Mocker.GetMock<INavigationService>();
        var tokenRespponse = new TokenResponse
        {
            Token = "access",
            RefreshToken = "refresh"
        };
        mockedAuthSvc.Setup(a => a.SignInAsync(It.IsAny<TokenRequest>())).ReturnsAsync(tokenRespponse);
        Sut.UserName = "test";
        Sut.Password = "pass";
        Sut.KeepMeLoggedIn = true;
        
        // Act
        await Sut.LoginCommand.ExecuteAsync(null);
        
        // Assert
        mockedSecureStorage.Verify(s => s.SetAsync(AppConstants.SecureStorage.AccessTokenKey, It.IsAny<string>()), Times.Once);
        mockedSecureStorage.Verify(s => s.SetAsync(AppConstants.SecureStorage.RefreshTokenKey, It.IsAny<string>()), Times.Once);
        mockedNavSvc.Verify(n => n.NavigateToAsync(string.Format(AppConstants.Navigation.NavStringFormat,
            AppConstants.Navigation.RootPagePrefix, NavigationRoute.TodoListPage.ToString()), false, null), Times.Once);
    }
}
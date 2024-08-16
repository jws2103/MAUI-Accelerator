using FluentAssertions;
using MauiAccelerator.Core.Api;
using MauiAccelerator.Core.Constants;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Features.Login.Models;
using MauiAccelerator.Features.Login.Services;
using Moq;

namespace MauiAccelerator.UnitTests.Features.Login;

public class AuthServiceTests : UnitTestBase<AuthService>
{
    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public async Task SignInSilentAsync_ShouldReturnFalse_WhenTokenIsNotAvailable(bool hasAccessToken, bool hasRefreshToken)
    {
        // Arrange
        var mockedSecureStorage = Mocker.GetMock<ISecureStorageService>();
        var accessToken = hasAccessToken ? "accessToken" : null;
        var refreshToken = hasRefreshToken ? "refreshToken" : null;
        mockedSecureStorage.Setup(s => s.GetAsync(AppConstants.SecureStorage.AccessTokenKey)).ReturnsAsync(accessToken);
        mockedSecureStorage.Setup(s => s.GetAsync(AppConstants.SecureStorage.RefreshTokenKey)).ReturnsAsync(refreshToken);
        
        // Act
        var response = await Sut.SignInSilentAsync();
        
        // Assert
        response.Should().BeFalse();
    }
    
    [Fact]
    public async Task SignInSilentAsync_ShouldRefreshToken_WhenTokenIsExpired()
    {
        // Arrange
        var mockedApi = Mocker.GetMock<IDummayApi>();
        var mockedSecureStorage = Mocker.GetMock<ISecureStorageService>();
        var accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwidXNlcm5hbWUiOiJlbWlseXMiLCJlbWFpbCI6ImVtaWx5LmpvaG5zb25AeC5kdW1teWpzb24uY29tIiwiZmlyc3ROYW1lIjoiRW1pbHkiLCJsYXN0TmFtZSI6IkpvaG5zb24iLCJnZW5kZXIiOiJmZW1hbGUiLCJpbWFnZSI6Imh0dHBzOi8vZHVtbXlqc29uLmNvbS9pY29uL2VtaWx5cy8xMjgiLCJpYXQiOjE3MjM3MjQ2MDgsImV4cCI6MTcyMzcyODIwOH0._KPXB4C7Ts83iIVmgyltBw-TB9VIzrBvQurMKgp_AFs";
        var newToken = "newToken";
        var refreshToken = "refreshToken";
        mockedSecureStorage.Setup(s => s.GetAsync(AppConstants.SecureStorage.AccessTokenKey)).ReturnsAsync(accessToken);
        mockedSecureStorage.Setup(s => s.GetAsync(AppConstants.SecureStorage.RefreshTokenKey)).ReturnsAsync(refreshToken);
        mockedApi.Setup(a => a.RefreshAuthTokenAsync(It.IsAny<RefreshTokenRequest>())).ReturnsAsync(new TokenResponse
        {
            Token = newToken,
            RefreshToken = refreshToken
        });
            
        // Act
        var response = await Sut.SignInSilentAsync();
        
        // Assert
        response.Should().BeTrue();
        mockedSecureStorage.Verify(s => s.SetAsync(AppConstants.SecureStorage.AccessTokenKey, newToken), Times.Once);
        mockedSecureStorage.Verify(s => s.SetAsync(AppConstants.SecureStorage.RefreshTokenKey, refreshToken), Times.Once);
    }
    
    [Fact]
    public async Task SignInAsync_ShouldVerifyGetAuthTokenAsync_WhenInvoked()
    {
        // Arrange
        var mockedApi = Mocker.GetMock<IDummayApi>();
        
        // Act
        await Sut.SignInAsync(It.IsAny<TokenRequest>());
        
        // Assert
        mockedApi.Verify(d => d.GetAuthTokenAsync(It.IsAny<TokenRequest>()), Times.Once);
    }
    
    [Fact]
    public async Task GetUserTokenAsync_ShouldVerifyGetAsync_WhenInvoked()
    {
        // Arrange
        var mockedSecureStorage = Mocker.GetMock<ISecureStorageService>();
        
        // Act
        await Sut.GetUserTokenAsync();
        
        // Assert
        mockedSecureStorage.Verify(s => s.GetAsync(AppConstants.SecureStorage.AccessTokenKey), Times.Once);
    }
}
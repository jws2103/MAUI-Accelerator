using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using MauiAccelerator.Core.Api;
using MauiAccelerator.Core.Constants;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Core.Extensions;
using MauiAccelerator.Features.Login.Models;

namespace MauiAccelerator.Features.Login.Services;

public class AuthService(IDummayApi api, ISecureStorageService secureStorageService) : IAuthService
{
    public async Task<TokenResponse?> SignInAsync(TokenRequest tokenRequest)
    {
        return await api.GetAuthTokenAsync(tokenRequest);
    }

    public async Task<bool> SignInSilentAsync()
    {
        var accessToken = await secureStorageService.GetAsync(AppConstants.SecureStorage.AccessTokenKey);
        var refreshToken = await secureStorageService.GetAsync(AppConstants.SecureStorage.RefreshTokenKey);
        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
        {
            return false;
        }

        var tokenExpired = HasTokenExpired(accessToken);
        if (!tokenExpired)
        {
            return true;
        }
        
        var newTokenResponse = await SilentSignInAsync(new RefreshTokenRequest
        {
            RefreshToken = refreshToken
        });

        if (newTokenResponse == null)
        {
            return false;
        }
        
        await secureStorageService.SetAsync(AppConstants.SecureStorage.AccessTokenKey, newTokenResponse.Token);
        await secureStorageService.SetAsync(AppConstants.SecureStorage.RefreshTokenKey, newTokenResponse.RefreshToken);
        return true;
    }

    public async Task<string?> GetUserTokenAsync()
    {
        return await secureStorageService.GetAsync(AppConstants.SecureStorage.AccessTokenKey);
    }

    private async Task<TokenResponse?> SilentSignInAsync(RefreshTokenRequest tokenRequest)
    {
        return await api.RefreshAuthTokenAsync(tokenRequest);
    }

    private bool HasTokenExpired(string accessToken)
    {
        var expired = true;
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(accessToken);
        var securityToken = jsonToken as JwtSecurityToken;
        var unixTimestampValue = securityToken?.Claims.FirstOrDefault(claim => claim.Type == AppConstants.SecureStorage.ExpiryDateKey)?.Value;
        var tokenExpiryDate = unixTimestampValue.ToUtcFromUnixTimestamp();
        if (tokenExpiryDate >= DateTime.UtcNow)
        {
            expired = false;
        }

        return expired;
    }
}
using System.Threading.Tasks;
using MauiAccelerator.Features.Login.Models;
using MauiAccelerator.Features.Profile.Models;
using Refit;

namespace MauiAccelerator.Core.Api;

public interface IDummayApi
{
    /* Use the sample credentials from https://dummyjson.com/users */
    [Post("/auth/login")]
    Task<TokenResponse> GetAuthTokenAsync([Body] TokenRequest tokenRequest);
    
    [Post("/auth/refresh")]
    Task<TokenResponse> RefreshAuthTokenAsync([Body] RefreshTokenRequest tokenRequest);

    [Get("/auth/me")]
    Task<UserDto> GetUserProfileAsync();
}
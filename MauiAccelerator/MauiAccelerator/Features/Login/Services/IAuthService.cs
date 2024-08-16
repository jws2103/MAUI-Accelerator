using System.Threading.Tasks;
using MauiAccelerator.Features.Login.Models;

namespace MauiAccelerator.Features.Login.Services;

public interface IAuthService
{
    Task<TokenResponse?> SignInAsync(TokenRequest tokenRequest);
    
    Task<bool> SignInSilentAsync();

    Task<string?> GetUserTokenAsync();
}
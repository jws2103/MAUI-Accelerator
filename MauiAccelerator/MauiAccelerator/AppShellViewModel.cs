using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using MauiAccelerator.Core.Constants;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Core.Navigation;
using MauiAccelerator.Features.Common.Pages;
using MauiAccelerator.Features.Login.Services;

namespace MauiAccelerator;

public partial class AppShellViewModel(
    INavigationService navigationService, 
    IPreferenceService preferenceService,
    IAuthService authService,
    ISecureStorageService secureStorageService) 
: BasePageViewModel(navigationService)
{
    private readonly INavigationService _navigationService = navigationService;

    public async Task NavigateToInitialScreen()
    {
        var hasSignedIn = await authService.SignInSilentAsync();
        var userLoggedIn = preferenceService.UserLoggedIn;
        var initialRoute = string.Format(AppConstants.Navigation.NavStringFormat,
            AppConstants.Navigation.RootPagePrefix, userLoggedIn && hasSignedIn ? NavigationRoute.TodoListPage.ToString() : NavigationRoute.LoginPage.ToString());

        await _navigationService.NavigateToAsync(initialRoute);
    }
    
    [RelayCommand]
    private async Task Logout()
    {
        // Remove tokens
        secureStorageService.Remove(AppConstants.SecureStorage.AccessTokenKey);
        secureStorageService.Remove(AppConstants.SecureStorage.RefreshTokenKey);
        
        // Navigate back to log in screen
        var loginRoute = string.Format(AppConstants.Navigation.NavStringFormat, AppConstants.Navigation.RootPagePrefix,
            NavigationRoute.LoginPage.ToString());
        await _navigationService.NavigateToAsync(loginRoute);
    }
}
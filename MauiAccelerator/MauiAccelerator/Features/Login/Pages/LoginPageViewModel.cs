using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAccelerator.Core.Constants;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Core.Navigation;
using MauiAccelerator.Features.Common.Pages;
using MauiAccelerator.Features.Login.Models;
using MauiAccelerator.Features.Login.Services;
using MauiAccelerator.Features.Popups.Loading.Pages;
using MauiAccelerator.Features.Popups.Loading.Services;
using Refit;

namespace MauiAccelerator.Features.Login.Pages;

public partial class LoginPageViewModel(
    INavigationService navigationService, 
    IAlertService alertService, 
    IPreferenceService preferenceService, 
    IAuthService authService,
    ILoadingService loadingService,
    ISecureStorageService secureStorageService)
    : BasePageViewModel(navigationService)
{
    [ObservableProperty] private string _userName;
    [ObservableProperty] private string _password;
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(ShowPasswordText))]
    private bool _isPassword = true;
    [ObservableProperty] private bool _keepMeLoggedIn;
    public string ShowPasswordText => IsPassword ? "Show" : "Hide";

    public override void OnAppearing()
    {
        base.OnAppearing();
        KeepMeLoggedIn = preferenceService.UserLoggedIn;
    }

    [RelayCommand]
    private async Task Login()
    {
        if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
        {
            await alertService.ShowAlertAsync(AppConstants.Alerts.Errors.ErrorHeading, AppConstants.Alerts.Errors.LoginErrorContent);
            return;
        }

        try
        {
            using (await loadingService.Show(new LoadingPopup()))
            {
                var tokenResponse = await authService.SignInAsync(new TokenRequest
                {
                    Username = UserName,
                    Password = Password
                });
                
                if (string.IsNullOrEmpty(tokenResponse?.Token))
                {
                    await alertService.ShowAlertAsync(AppConstants.Alerts.Errors.ErrorHeading, AppConstants.Alerts.Errors.LoginFailedErrorContent);
                    return;
                }

                if (!string.IsNullOrEmpty(tokenResponse.Token) && !string.IsNullOrEmpty(tokenResponse.RefreshToken))
                {
                    await secureStorageService.SetAsync(AppConstants.SecureStorage.AccessTokenKey, tokenResponse.Token);
                    await secureStorageService.SetAsync(AppConstants.SecureStorage.RefreshTokenKey, tokenResponse.RefreshToken);                    
                }

                preferenceService.UserLoggedIn = KeepMeLoggedIn;
                Password = string.Empty;
                var todoListRoute = string.Format(AppConstants.Navigation.NavStringFormat,
                    AppConstants.Navigation.RootPagePrefix, NavigationRoute.TodoListPage.ToString());
                await NavigationService.NavigateToAsync(todoListRoute);
            }
        }
        catch (ApiException exception)
        {
            Debug.WriteLine(exception);
            await alertService.ShowAlertAsync(AppConstants.Alerts.Errors.ErrorHeading, AppConstants.Alerts.Errors.LoginFailedErrorContent);
        }
    }
    
    [RelayCommand]
    private void TogglePassword()
    {
        IsPassword = !IsPassword;
    }
}
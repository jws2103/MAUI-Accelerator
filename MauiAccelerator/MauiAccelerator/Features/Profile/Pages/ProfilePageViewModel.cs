using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAccelerator.Core.Extensions;
using MauiAccelerator.Core.Navigation;
using MauiAccelerator.Features.Common.Pages;
using MauiAccelerator.Features.Popups.Loading.Pages;
using MauiAccelerator.Features.Popups.Loading.Services;
using MauiAccelerator.Features.Profile.Models;
using MauiAccelerator.Features.Profile.Services;
using Refit;

namespace MauiAccelerator.Features.Profile.Pages;

public partial class ProfilePageViewModel(
    INavigationService navigationService, 
    IProfileService profileService,
    ILoadingService loadingService)
    : BasePageViewModel(navigationService)
{
    [ObservableProperty] private UserDto _userProfile;

    [ObservableProperty] private int _selectedTabIndex;
    
    public IList<string> TabItemTexts => new List<string>
    {
        "User info",
        "Address",
        "Company"
    };
    
    public override void OnAppearing()
    {
        base.OnAppearing();
        LoadProfile().Forget();
    }

    [RelayCommand]
    private void SelectedTabChanged(int tabIndex)
    {
        SelectedTabIndex = tabIndex;
    }

    private async Task LoadProfile()
    {
        try
        {
            using (await loadingService.Show(new LoadingPopup()))
            {
                UserProfile = await profileService.GetUserProfileAsync();
            }
        }
        catch (ApiException exception)
        {
            Debug.WriteLine(exception);
        }
    }
}
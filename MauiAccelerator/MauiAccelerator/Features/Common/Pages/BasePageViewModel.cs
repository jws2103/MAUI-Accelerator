using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAccelerator.Core.Navigation;

namespace MauiAccelerator.Features.Common.Pages;

public partial class BasePageViewModel(INavigationService navigationService) : ObservableObject
{
    protected INavigationService NavigationService = navigationService;

    public virtual void OnAppearing()
    {
    }
    
    public virtual void OnDisappearing()
    {
    }
    
    public virtual void OnNavigatedTo()
    {
    }

    public virtual void OnNavigatingFrom()
    {
    }

    public virtual void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private async Task ShellGoBackAsync()
    {
        await NavigationService.GoBackAsync();
    }
}
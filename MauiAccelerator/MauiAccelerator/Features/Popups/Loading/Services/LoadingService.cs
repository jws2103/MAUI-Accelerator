using Mopups.Interfaces;
using Mopups.Pages;

namespace MauiAccelerator.Features.Popups.Loading.Services;

public class LoadingService(IPopupNavigation popupNavigation) : ILoadingService, IDisposable
{
    public async Task<IDisposable> Show(PopupPage page)
    {
        await popupNavigation.PushAsync(page);
        return this;
    }

    public async void Dispose()
    {
        await popupNavigation.PopAsync();
    }
}
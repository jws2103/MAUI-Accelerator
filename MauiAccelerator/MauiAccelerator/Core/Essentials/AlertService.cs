using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MauiAccelerator.Core.Essentials;

[ExcludeFromCodeCoverage]
public class AlertService : IAlertService
{
    public Task ShowAlertAsync(string title, string message, string cancel = Constants.AppConstants.Alerts.OK)
    {
        return Shell.Current.DisplayAlert(title, message, cancel);
    }

    public Task<bool> ShowConfirmationAsync(string title, string message, string accept = Constants.AppConstants.Alerts.Yes, string cancel = Constants.AppConstants.Alerts.No)
    {
        return Shell.Current.DisplayAlert(title, message, accept, cancel);
    }
    
    public void ShowAlert(string title, string message, string cancel = Constants.AppConstants.Alerts.OK)
    {
        Shell.Current.Dispatcher.Dispatch(async () =>
            await ShowAlertAsync(title, message, cancel)
        );
    }
    
    public void ShowConfirmation(string title, string message, Action<bool> callback,
        string accept=Constants.AppConstants.Alerts.Yes, string cancel = Constants.AppConstants.Alerts.No)
    {
        Shell.Current.Dispatcher.Dispatch(async () =>
        {
            bool answer = await ShowConfirmationAsync(title, message, accept, cancel);
            callback(answer);
        });
    }
}
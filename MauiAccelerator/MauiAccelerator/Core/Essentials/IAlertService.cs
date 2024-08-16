using System;
using System.Threading.Tasks;

namespace MauiAccelerator.Core.Essentials;

public interface IAlertService
{
    Task ShowAlertAsync(string title, string message, string cancel = Constants.AppConstants.Alerts.OK);
    Task<bool> ShowConfirmationAsync(string title, string message, string accept = Constants.AppConstants.Alerts.Yes, string cancel = Constants.AppConstants.Alerts.No);
    void ShowAlert(string title, string message, string cancel = Constants.AppConstants.Alerts.OK);
    void ShowConfirmation(string title, string message, Action<bool> callback, string accept = Constants.AppConstants.Alerts.Yes, string cancel = Constants.AppConstants.Alerts.No);
}
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MauiAccelerator.Core.Constants;
using Microsoft.Maui.Controls;

namespace MauiAccelerator.Core.Navigation;

[ExcludeFromCodeCoverage]
public class NavigationService : INavigationService
{
    public Task NavigateToAsync(string route, bool animate = false, IDictionary<string, object>? routeParameters = null)
    {
        return routeParameters != null 
            ? Shell.Current.GoToAsync(route, animate, routeParameters) 
            : Shell.Current.GoToAsync(route, animate);
    }

    public Task GoBackAsync(bool animate = false)
    {
        return Shell.Current.GoToAsync(AppConstants.Navigation.NavBackPrefix, animate);
    }
}
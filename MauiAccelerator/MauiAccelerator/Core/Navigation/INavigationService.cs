using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiAccelerator.Core.Navigation;

public interface INavigationService
{
    Task NavigateToAsync(string route, bool animate = false, IDictionary<string, object>? routeParameters = null);

    Task GoBackAsync(bool animate = false);
}
using System.Diagnostics.CodeAnalysis;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Core.Navigation;
using MauiAccelerator.Features.Login.Pages;
using MauiAccelerator.Features.Login.Services;
using MauiAccelerator.Features.Todo.Pages;

namespace MauiAccelerator;

[ExcludeFromCodeCoverage]
public partial class AppShell : Shell
{
    private readonly AppShellViewModel _viewModel;
    
    public AppShell(IServiceProvider provider)
    {
        InitializeComponent();
        RegisterRoutes();
        _viewModel = new AppShellViewModel(provider.GetService<INavigationService>()!, provider.GetService<IPreferenceService>()!, provider.GetService<IAuthService>()!, provider.GetService<ISecureStorageService>()!);
        BindingContext = _viewModel;
    }

    protected override async void OnParentSet()
    {
        base.OnParentSet();
        
        if (Parent is not null)
        {
            await _viewModel.NavigateToInitialScreen()!;
        }
    }

    private void RegisterRoutes()
    {
        var routes = new Dictionary<string, Type>
        {
            { NavigationRoute.LoginPage.ToString(), typeof(LoginPage) },
            { NavigationRoute.TodoListPage.ToString(), typeof(TodoListPage) },
            { NavigationRoute.TodoItemPage.ToString(), typeof(TodoItemPage) }
        };

        foreach (var item in routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }
    }
}
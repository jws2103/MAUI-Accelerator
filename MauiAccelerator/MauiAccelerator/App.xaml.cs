using System.Diagnostics.CodeAnalysis;

namespace MauiAccelerator;

[ExcludeFromCodeCoverage]
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }
    
    public App(IServiceProvider provider)
    {
        InitializeComponent();
        MainPage = new AppShell(provider);
    }
}
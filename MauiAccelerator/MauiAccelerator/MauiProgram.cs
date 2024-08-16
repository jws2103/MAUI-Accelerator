using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using CommunityToolkit.Maui;
using MauiAccelerator.Core.Api;
using MauiAccelerator.Core.Database;
using MauiAccelerator.Core.Device;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Core.Messenger;
using MauiAccelerator.Core.Navigation;
using MauiAccelerator.Core.Settings;
using MauiAccelerator.Features.Login.Pages;
using MauiAccelerator.Features.Login.Services;
using MauiAccelerator.Features.Popups.Loading.Pages;
using MauiAccelerator.Features.Popups.Loading.Services;
using MauiAccelerator.Features.Profile.Pages;
using MauiAccelerator.Features.Profile.Services;
using MauiAccelerator.Features.Todo.Pages;
using MauiAccelerator.UI.Font;

#if  IOS
using MauiAccelerator.iOS.Services;
#elif ANDROID
using MauiAccelerator.Droid.Services;
#endif
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Mopups.Hosting;
using Mopups.Services;
using Plugin.Maui.SegmentedControl;
using Refit;

namespace MauiAccelerator;

[ExcludeFromCodeCoverage]
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", FontFamily.OpenSansRegular);
            fonts.AddFont("OpenSans-Semibold.ttf", FontFamily.OpenSansSemibold);
            fonts.AddFont("FontAwesome6ProLight.otf", FontFamily.FALight);
            fonts.AddFont("FontAwesome6ProRegular.otf", FontFamily.FARegular);
            fonts.AddFont("FontAwesome6ProSolid.otf", FontFamily.FASolid);
            fonts.AddFont("FontAwesome6ProThin.otf", FontFamily.FAThin);
        })
            .UseMauiCommunityToolkit()
            .ConfigureMopups()
            .UseSegmentedControl();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif
        // Register pages with viewmodel
        builder.Services.AddTransient<LoginPage, LoginPageViewModel>();
        builder.Services.AddTransient<TodoListPage, TodoListPageViewModel>();
        builder.Services.AddTransient<TodoItemPage, TodoItemPageViewModel>();
        builder.Services.AddTransient<ProfilePage, ProfilePageViewModel>();
        builder.Services.AddTransient<LoadingPopup>();
        
        // Register services
        builder.Services.AddSingleton(MopupService.Instance);
        builder.Services.AddSingleton<ILoadingService, LoadingService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IPreferenceService, PreferenceService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();
        builder.Services.AddSingleton<IMessengerService, MessengerService>();
        builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();
        builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();
#if IOS
        builder.Services.AddSingleton<IDeviceService, DeviceServiceiOS>();
#elif ANDROID
        builder.Services.AddSingleton<IDeviceService, DeviceServiceDroid>();
#endif
        
        // Add the feature specific services
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddSingleton<IProfileService, ProfileService>();
        
        // Register repositories
        builder.Services.AddSingleton(DatabaseFactory.BuildDatabaseRepository());

        // Load appsettings.json
        var appSettings = AppSettingsFactory.LoadAppSettingsJson();
        builder.Services.AddSingleton(appSettings);
            
        // Build the api and service
        builder.Services
            .AddRefitClient<IDummayApi>(new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    WriteIndented = true
                })
            })
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(appSettings.ApiBaseUri ?? string.Empty))
            .ConfigurePrimaryHttpMessageHandler(ApiFactory.BuildHttpMessageHandlers);
        
        return builder.Build();
    }
}
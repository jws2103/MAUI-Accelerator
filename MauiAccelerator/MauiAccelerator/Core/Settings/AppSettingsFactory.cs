using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using MauiAccelerator.Core.Constants;
using MauiAccelerator.Core.Settings.Models;
using Microsoft.Extensions.Configuration;

namespace MauiAccelerator.Core.Settings;

[ExcludeFromCodeCoverage]
public static class AppSettingsFactory
{
    public static IAppSettings LoadAppSettingsJson()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(AppConstants.AppSettingsNamespace);
        if (stream == null)
        {
            return new AppSettings();
        }
        
        var configuration = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        return configuration.Get<AppSettings>() ?? new();

    }
}
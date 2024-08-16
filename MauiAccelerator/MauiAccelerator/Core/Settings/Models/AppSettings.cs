namespace MauiAccelerator.Core.Settings.Models;

public class AppSettings : IAppSettings
{
    public string? Environment { get; set; }

    public string? ApiBaseUri { get; set; }
}
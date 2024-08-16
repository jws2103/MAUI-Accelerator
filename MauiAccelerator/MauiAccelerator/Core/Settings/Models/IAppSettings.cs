namespace MauiAccelerator.Core.Settings.Models;

public interface IAppSettings
{
    string? Environment { get; }

    string? ApiBaseUri { get; }
}
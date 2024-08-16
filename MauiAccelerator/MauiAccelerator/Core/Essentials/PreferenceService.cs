using System.Diagnostics.CodeAnalysis;
using MauiAccelerator.Core.Constants;
using Microsoft.Maui.Storage;

namespace MauiAccelerator.Core.Essentials;

[ExcludeFromCodeCoverage]
public sealed class PreferenceService : IPreferenceService
{
    private const string UserLoggedInKey = "user_loggedin";
    private const bool UserLoggedInDefault = false;
    
    public bool UserLoggedIn
    {
        get => Preferences.Default.Get(UserLoggedInKey, UserLoggedInDefault);
        set => Preferences.Default.Set(UserLoggedInKey, value);
    }
}
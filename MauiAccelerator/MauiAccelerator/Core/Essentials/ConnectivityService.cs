using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Maui.Networking;

namespace MauiAccelerator.Core.Essentials;

[ExcludeFromCodeCoverage]
public class ConnectivityService : IConnectivityService
{
    public ConnectivityService()
    {
        Connectivity.Current.ConnectivityChanged += CurrentOnConnectivityChanged;
    }

    public event EventHandler<bool>? NetworkStatusChanged;
    
    public bool IsConnected => Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
    
    private void CurrentOnConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        var networkAccess = e.NetworkAccess;
        NetworkStatusChanged?.Invoke(this, networkAccess == NetworkAccess.Internet);
    }
}
using System;

namespace MauiAccelerator.Core.Essentials;

public interface IConnectivityService
{
    event EventHandler<bool>? NetworkStatusChanged;
    
    bool IsConnected { get; }
}
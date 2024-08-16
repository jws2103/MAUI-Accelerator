using System;
using System.Net.Http;
using MauiAccelerator.Core.Api.Handlers;
using MauiAccelerator.Core.Device;
using MauiAccelerator.Core.Essentials;
using Microsoft.Extensions.DependencyInjection;

namespace MauiAccelerator.Core.Api;

public static class ApiFactory
{
    public static HttpMessageHandler BuildHttpMessageHandlers(IServiceProvider provider)
    {
        var connectivityService = provider.GetService<IConnectivityService>();
        var deviceService = provider.GetService<IDeviceService>();
        var nativeHandler = deviceService!.GetNativeHttpMessageHandler();
        return new AuthenticationHandler(provider, new TimeoutHandler(
                new ConnectivityHandler(connectivityService, nativeHandler)
            ));
    }
}
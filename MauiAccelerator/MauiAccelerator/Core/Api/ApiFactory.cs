using System;
using System.Net.Http;
using MauiAccelerator.Core.Api.Handlers;
using MauiAccelerator.Core.Constants;
using MauiAccelerator.Core.Device;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Core.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MauiAccelerator.Core.Api;

public static class ApiFactory
{
    public static HttpMessageHandler BuildHttpMessageHandlers(IServiceProvider provider)
    {
        var connectivityService = provider.GetService<IConnectivityService>();
        var deviceService = provider.GetService<IDeviceService>();
        var nativeHandler = deviceService!.GetNativeHttpMessageHandler();
        
        if (DebugHelper.IsDebug())
        {
            var loggerProvider = provider.GetService<ILoggerProvider>();
            var logger = loggerProvider?.CreateLogger(AppConstants.ApiLoggingCategory);
            return new AuthenticationHandler(provider, 
                new TimeoutHandler(
                    new ConnectivityHandler(connectivityService, 
                        new LoggingHandler(logger, false, nativeHandler))
                ));
        }

        return new AuthenticationHandler(provider, 
            new TimeoutHandler(
                new ConnectivityHandler(connectivityService, nativeHandler)
            ));
    }
}
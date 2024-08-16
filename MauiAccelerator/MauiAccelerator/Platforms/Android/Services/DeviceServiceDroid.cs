using MauiAccelerator.Core.Device;
using Xamarin.Android.Net;

namespace MauiAccelerator.Droid.Services;

public class DeviceServiceDroid : IDeviceService
{
    public HttpMessageHandler GetNativeHttpMessageHandler()
    {
        return new AndroidMessageHandler();
    }
}
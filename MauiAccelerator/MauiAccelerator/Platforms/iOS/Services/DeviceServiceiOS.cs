using MauiAccelerator.Core.Device;

namespace MauiAccelerator.iOS.Services;

public class DeviceServiceiOS : IDeviceService
{
    public HttpMessageHandler GetNativeHttpMessageHandler()
    {
        return new NSUrlSessionHandler();
    }
}
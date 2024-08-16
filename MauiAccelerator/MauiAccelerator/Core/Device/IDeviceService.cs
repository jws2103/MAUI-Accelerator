using System.Net.Http;

namespace MauiAccelerator.Core.Device;

public interface IDeviceService
{
    HttpMessageHandler GetNativeHttpMessageHandler();
}
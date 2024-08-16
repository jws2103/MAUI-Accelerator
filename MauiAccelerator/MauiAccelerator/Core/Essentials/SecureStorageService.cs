using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace MauiAccelerator.Core.Essentials;

[ExcludeFromCodeCoverage]
public class SecureStorageService : ISecureStorageService
{
    public Task<string?> GetAsync(string key)
    {
        return SecureStorage.Default.GetAsync(key);
    }

    public Task SetAsync(string key, string value)
    {
        return SecureStorage.Default.SetAsync(key, value);
    }

    public void Remove(string key)
    {
        SecureStorage.Default.Remove(key);
    }

    public void RemoveAll()
    {
        SecureStorage.Default.RemoveAll();
    }
}
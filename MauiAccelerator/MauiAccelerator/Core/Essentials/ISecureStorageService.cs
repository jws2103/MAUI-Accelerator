using System.Threading.Tasks;

namespace MauiAccelerator.Core.Essentials;

public interface ISecureStorageService
{
    Task<string?> GetAsync(string key);

    Task SetAsync(string key, string value);

    void Remove(string key);

    void RemoveAll();
}
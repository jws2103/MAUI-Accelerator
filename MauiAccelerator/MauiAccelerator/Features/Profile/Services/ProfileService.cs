using System.Threading.Tasks;
using MauiAccelerator.Core.Api;
using MauiAccelerator.Features.Profile.Models;

namespace MauiAccelerator.Features.Profile.Services;

public class ProfileService(IDummayApi api) : IProfileService
{
    public async Task<UserDto> GetUserProfileAsync()
    {
        return await api.GetUserProfileAsync();
    }
}
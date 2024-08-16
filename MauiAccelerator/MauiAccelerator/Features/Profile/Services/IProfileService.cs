using System.Threading.Tasks;
using MauiAccelerator.Features.Profile.Models;

namespace MauiAccelerator.Features.Profile.Services;

public interface IProfileService
{
    Task<UserDto> GetUserProfileAsync();
}
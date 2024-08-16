using System;
using System.Threading.Tasks;
using Mopups.Pages;

namespace MauiAccelerator.Features.Popups.Loading.Services;

public interface ILoadingService
{
    Task<IDisposable> Show(PopupPage page);
}
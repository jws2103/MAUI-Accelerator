using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MauiAccelerator.Core.Essentials;
using MauiAccelerator.Core.Exceptions;

namespace MauiAccelerator.Core.Api.Handlers;

[ExcludeFromCodeCoverage]
public class ConnectivityHandler(IConnectivityService? connectivityService, HttpMessageHandler innerHandler)
    : DelegatingHandler(innerHandler)
{
    private readonly IConnectivityService _connectivityService = connectivityService ?? throw new ArgumentNullException(nameof(connectivityService));

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!_connectivityService.IsConnected)
        {
            throw new NoConnectivityException();
        }
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
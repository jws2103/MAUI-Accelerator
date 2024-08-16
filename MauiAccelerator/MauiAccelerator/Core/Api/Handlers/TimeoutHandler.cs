using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MauiAccelerator.Core.Constants;

namespace MauiAccelerator.Core.Api.Handlers;

[ExcludeFromCodeCoverage]
public class TimeoutHandler(
    HttpMessageHandler innerHandler,
    double timeoutInSeconds = AppConstants.DefaultTimeoutInSeconds)
    : DelegatingHandler(innerHandler)
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        using var cts = GetCancellationTokenSource(cancellationToken);
        try
        {
            return await base.SendAsync(request, cts?.Token ?? cancellationToken);
        }
        catch(OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            throw new TimeoutException();
        }
    }
        
    private CancellationTokenSource GetCancellationTokenSource(CancellationToken cancellationToken)
    {
        var cts = CancellationTokenSource
            .CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));
        return cts;
    }
}

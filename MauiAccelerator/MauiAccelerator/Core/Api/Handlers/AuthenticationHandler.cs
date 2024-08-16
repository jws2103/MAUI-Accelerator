using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MauiAccelerator.Core.Messenger;
using MauiAccelerator.Core.Messenger.Messages;
using MauiAccelerator.Features.Login.Services;

namespace MauiAccelerator.Core.Api.Handlers;

public class AuthenticationHandler(IServiceProvider provider, HttpMessageHandler innerHandler)
    : DelegatingHandler(innerHandler)
{
    private const string BearerAuthKey = "Bearer";

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Bypass any auth endpoints
        var absoluteUrlPath = request.RequestUri!.AbsoluteUri;
        if (absoluteUrlPath.Contains("/login") || 
            absoluteUrlPath.Contains("/refresh"))
        {
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        
        // Add the bearer token
        var authService = provider.GetService<IAuthService>();
        var bearerToken = await authService!.GetUserTokenAsync().ConfigureAwait(false);
        if (!string.IsNullOrWhiteSpace(bearerToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(BearerAuthKey, bearerToken);
        }

        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var httpStatusCode = response.StatusCode;
        if (httpStatusCode != HttpStatusCode.Unauthorized)
        {
            return response;
        }
        
        var success = await authService.SignInSilentAsync();
        if (!success)
        {
            var messengerService = provider.GetService<IMessengerService>();
            messengerService!.Send(new UserLoggedOutMessage());
        }
        else
        {
            response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        return response;
    }
}

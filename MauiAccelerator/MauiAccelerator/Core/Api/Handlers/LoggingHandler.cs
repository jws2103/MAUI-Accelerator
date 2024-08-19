using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MauiAccelerator.Core.Api.Handlers;

    [ExcludeFromCodeCoverage]
    public class LoggingHandler(ILogger? logger, bool logResponseContent = true, HttpMessageHandler innerHandler = null)
        : DelegatingHandler(innerHandler ?? new HttpClientHandler())
    {
        readonly string[] types = { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid().ToString();
            var msg = $"[{id} - Request]";
            
            logger?.Log(LogLevel.Debug, $"{msg}========Start==========");
            logger?.Log(LogLevel.Debug, $"{msg} {request.Method} {request.RequestUri.PathAndQuery} {request.RequestUri.Scheme}/{request.Version}");
            logger?.Log(LogLevel.Debug, $"{msg} Host: {request.RequestUri.Scheme}://{request.RequestUri.Host}");

            foreach (var header in request.Headers)
            {
                logger?.Log(LogLevel.Debug, $"{msg} {header.Key}: {string.Join(", ", header.Value)}");
            }
            
            if (request.Content != null)
            {
                foreach (var header in request.Content.Headers)
                {
                    logger?.Log(LogLevel.Debug, $"{msg} {header.Key}: {string.Join(", ", header.Value)}");
                }
                
                if (request.Content is StringContent || IsTextBasedContentType(request.Headers) || IsTextBasedContentType(request.Content.Headers))
                {   
                    var result = await request.Content.ReadAsStringAsync();

                    logger?.Log(LogLevel.Debug, $"{msg} Content:");
                    logger?.Log(LogLevel.Debug, FormatJson(result));
                }
            }

            var start = DateTime.Now;

            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            var end = DateTime.Now;

            logger?.Log(LogLevel.Debug, $"{msg} Duration: {end - start}");
            logger?.Log(LogLevel.Debug, $"{msg}==========End==========");

            msg = $"[{id} - Response]";
            logger?.Log(LogLevel.Debug, $"{msg}=========Start=========");

            logger?.Log(LogLevel.Debug, $"{msg} {request.RequestUri.Scheme.ToUpper()}/{response.Version} {(int)response.StatusCode} {response.ReasonPhrase}");

            foreach (var header in response.Headers)
            {
                logger?.Log(LogLevel.Debug, $"{msg} {header.Key}: {string.Join(", ", header.Value)}");
            }
            

            if (response?.Content != null && logResponseContent)
            {
                foreach (var header in response.Content.Headers)
                {
                    logger?.Log(LogLevel.Debug, $"{msg} {header.Key}: {string.Join(", ", header.Value)}");
                }
                

                if (response.Content is StringContent || this.IsTextBasedContentType(response.Headers) || this.IsTextBasedContentType(response.Content.Headers))
                {
                    start = DateTime.Now;
                    var result = await response.Content.ReadAsStringAsync();
                    end = DateTime.Now;

                    logger?.Log(LogLevel.Debug, $"{msg} Content:");
                    logger?.Log(LogLevel.Debug, FormatJson(result));
                    logger?.Log(LogLevel.Debug, $"{msg} Duration: {end - start}");
                }
            }

            logger?.Log(LogLevel.Debug, $"{msg}==========End==========");
            return response;
        }

        private static string FormatJson(string json)
        {
            var parsedJson = JsonSerializer.Deserialize(json, typeof(object));
            if (parsedJson == null)
            {
                return string.Empty;
            }
            
            return JsonSerializer.Serialize(parsedJson, new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                WriteIndented = true
            });
        }

        private bool IsTextBasedContentType(HttpHeaders headers)
        {
            if (!headers.TryGetValues("Content-Type", out var values))
            {
                return false;
            }

            var header = string.Join(" ", values).ToLowerInvariant();

            return types.Any(t => header.Contains(t));
        }
    }
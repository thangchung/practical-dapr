using System;
using System.Text.Json;
using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace N8T.Infrastructure.Dapr
{
    public static class Extensions
    {
        public static DaprClient GetDaprClient(
            this IConfiguration config,
            string appId,
            bool isHttps = false,
            ILogger logger = null)
        {
            var url = GetDaprClientUrl(config, appId, isHttps);
            logger?.LogInformation($"Dapr Client Url: {url}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var client = new DaprClientBuilder()
                .UseEndpoint(url)
                .UseJsonSerializationOptions(options)
                .Build();

            return client;
        }

        public static string GetDaprClientUrl(this IConfiguration config, string appId, bool isHttps = false)
        {
            string host, port, url;
            if (!isHttps)
            {
                host = config[$"service:{appId}:host"];
                port = config[$"service:{appId}:port"];
                url = $"http://{host}:{port}";
            }
            else
            {
                var protocol = Environment.GetEnvironmentVariable($"{appId.ToUpper()}_HTTPS_SERVICE_PROTOCOL");
                port = Environment.GetEnvironmentVariable($"{appId.ToUpper()}_HTTPS_SERVICE_PORT");
                host = config[$"service:{appId}:host"];
                url = $"{protocol}://{host}:{port}";
            }

            return url;
        }
    }
}

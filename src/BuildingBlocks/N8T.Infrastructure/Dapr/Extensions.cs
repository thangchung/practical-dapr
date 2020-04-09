using System.Text.Json;
using Dapr.Client;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace N8T.Infrastructure.Dapr
{
    public static class Extensions
    {
        public static DaprClient GetDaprClient(
            this IConfiguration config,
            string appId,
            ILogger logger = null)
        {
            var url = GetDaprClientUrl(config, appId);
            logger?.LogInformation($"Dapr Client Url: {url}");

            var client = new DaprClientBuilder()
                .UseEndpoint(url)
                .UseJsonSerializationOptions(new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })
                .UseGrpcChannelOptions(new GrpcChannelOptions {Credentials = ChannelCredentials.Insecure})
                .Build();

            return client;
        }

        public static string GetDaprClientUrl(this IConfiguration config, string appId)
        {
            var host = config[$"service:{appId}:host"];
            var port = config[$"service:{appId}:port"];
            var url = $"http://{host}:{port}";
            return url;
        }
    }
}

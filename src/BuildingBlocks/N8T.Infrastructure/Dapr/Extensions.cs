using System.Text.Json;
using Dapr.Client;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using N8T.Infrastructure.Tye;

namespace N8T.Infrastructure.Dapr
{
    public static class Extensions
    {
        public static DaprClient GetDaprClient(
            this IConfiguration config,
            string appId,
            ILogger logger = null)
        {
            var url = config.GetTyeGrpcAppUrl(appId);
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
    }
}

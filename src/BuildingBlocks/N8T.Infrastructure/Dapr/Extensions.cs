using System.Text.Json;
using Dapr.Client;
using Microsoft.Extensions.DependencyInjection;

namespace N8T.Infrastructure.Dapr
{
    public static class Extensions
    {
        public static DaprClient GetDaprClient(this string endpoint)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var client = new DaprClientBuilder()
                .UseEndpoint(endpoint)
                .UseJsonSerializationOptions(options)
                .Build();

            return client;
        }
    }
}
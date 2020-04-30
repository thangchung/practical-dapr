using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Domain;
using Dapr.Client;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoolStore.ProductCatalogApi.Application.Publishers.PublishProductCreated
{
    public class PublishProductCreatedHandler
        : INotificationHandler<ProductCreated>
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<PublishProductCreatedHandler> _logger;

        public PublishProductCreatedHandler(
            DaprClient daprClient,
            ILogger<PublishProductCreatedHandler> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        public async Task Handle(
            ProductCreated notification,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"We got {JsonSerializer.Serialize(notification)}");

            await _daprClient.PublishEventAsync("productCreated", notification);
        }
    }
}

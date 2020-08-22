using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Domain;
using Dapr.Client;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoolStore.ProductCatalogApi.Application.Publishers.PublishProductCreated
{
    public class ProductCatalogPublisher
        : INotificationHandler<ProductCreated>
            , INotificationHandler<ProductUpdated>
            , INotificationHandler<ProductDeleted>
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<ProductCatalogPublisher> _logger;

        public ProductCatalogPublisher(
            DaprClient daprClient,
            ILogger<ProductCatalogPublisher> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        public async Task Handle(ProductCreated notification, CancellationToken token)
        {
            _logger.LogInformation(
                $"Publish the message {nameof(notification)}: {JsonSerializer.Serialize(notification)}");

            await _daprClient.PublishEventAsync("pubsub", "product-created", notification, token);
        }

        public async Task Handle(ProductUpdated notification, CancellationToken token)
        {
            _logger.LogInformation(
                $"Publish the message {nameof(notification)}: {JsonSerializer.Serialize(notification)}");

            await _daprClient.PublishEventAsync("pubsub", "product-updated", notification, token);
        }

        public async Task Handle(ProductDeleted notification, CancellationToken token)
        {
            _logger.LogInformation(
                $"Publish the message {nameof(notification)}: {JsonSerializer.Serialize(notification)}");

            await _daprClient.PublishEventAsync("pubsub", "product-deleted", notification, token);
        }
    }
}

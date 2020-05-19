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

        public async Task Handle(
            ProductCreated notification,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"We publish the message {nameof(notification)}: {JsonSerializer.Serialize(notification)}");
            await _daprClient.PublishEventAsync("product-created", notification);
        }

        public async Task Handle(
            ProductUpdated notification,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"We publish the message {nameof(notification)}: {JsonSerializer.Serialize(notification)}");
            await _daprClient.PublishEventAsync("product-updated", notification);
        }

        public async Task Handle(
            ProductDeleted notification,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"We publish the message {nameof(notification)}: {JsonSerializer.Serialize(notification)}");
            await _daprClient.PublishEventAsync("product-deleted", notification);
        }
    }
}

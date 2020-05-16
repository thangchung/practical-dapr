using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Domain;
using Dapr.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using N8T.Infrastructure;

namespace CoolStore.ProductCatalogApi.Application.Publishers.PublishProductCreated
{
    public class ProductCatalogPublisher
        : INotificationHandler<ProductCreated>
        , INotificationHandler<ProductUpdated>
        , INotificationHandler<ProductDeleted>
    {
        private readonly DaprClient _daprClient;
        private readonly IConfiguration _config;
        private readonly ILogger<ProductCatalogPublisher> _logger;

        public ProductCatalogPublisher(
            DaprClient daprClient,
            IConfiguration config,
            ILogger<ProductCatalogPublisher> logger)
        {
            _daprClient = daprClient;
            _config = config;
            _logger = logger;
        }

        public async Task Handle(
            ProductCreated notification,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"We publish the message {nameof(notification)}: {JsonSerializer.Serialize(notification)}");
            //await _daprClient.PublishEventAsync("product-created", notification);

            // try to invoke the binding
            await _daprClient.InvokeBindingAsync("sample", notification, null, cancellationToken);

            // var httpClient = new HttpClient();
            // httpClient.BaseAddress = _config.GetServiceUri(Consts.INVENTORY_API_ID);
            //
            // var json = JsonSerializer.Serialize(notification);
            // var data = new StringContent(json, Encoding.UTF8, "application/json");
            //
            // await httpClient.PostAsync("/v1.0/bindings/sample-topic", data);
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

using MediatR;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CoolStore.ProductCatalogApi.UseCases.ProductCreated
{
    public class ProductCreatedHandler : INotificationHandler<Protobuf.ProductCatalog.V1.ProductCreated>
    {
        private readonly ILogger<ProductCreatedHandler> _logger;

        public ProductCreatedHandler(ILogger<ProductCreatedHandler> logger)
        {
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
        }

        public Task Handle(Protobuf.ProductCatalog.V1.ProductCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"We got {JsonSerializer.Serialize(notification)}");
            return Task.CompletedTask;
        }
    }
}
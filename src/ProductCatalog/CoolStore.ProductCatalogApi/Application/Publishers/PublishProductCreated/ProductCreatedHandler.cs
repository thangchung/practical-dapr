using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoolStore.ProductCatalogApi.Application.Publishers.PublishProductCreated
{
    public class PublishProductCreatedHandler : INotificationHandler<ProductCreated>
    {
        private readonly ILogger<PublishProductCreatedHandler> _logger;

        public PublishProductCreatedHandler(ILogger<PublishProductCreatedHandler> logger)
        {
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
        }

        public Task Handle(ProductCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"We got {JsonSerializer.Serialize(notification)}");
            return Task.CompletedTask;
        }
    }
}

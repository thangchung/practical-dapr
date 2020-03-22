using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoolStore.ProductCatalogApi.Application.UseCase.PublishProductCreated
{
    public class PublishProductCreatedHandler : INotificationHandler<Protobuf.ProductCatalog.V1.ProductCreated>
    {
        private readonly ILogger<PublishProductCreatedHandler> _logger;

        public PublishProductCreatedHandler(ILogger<PublishProductCreatedHandler> logger)
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
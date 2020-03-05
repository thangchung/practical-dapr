using MediatR;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CoolStore.ProductCatalogApi.UseCases.ProductCreated
{
    public class ProductCreatedHandler : INotificationHandler<Protobuf.ProductCatalog.V1.ProductCreated>
    {
        public Task Handle(Protobuf.ProductCatalog.V1.ProductCreated notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"We got {JsonSerializer.Serialize(notification)}");
            return Task.CompletedTask;
        }
    }
}
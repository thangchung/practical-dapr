using CoolStore.ProductCatalogApi.Application.UseCase.GetProducts;
using CoolStore.Protobuf.Inventory.V1;
using CoolStore.Protobuf.ProductCatalog.V1;
using Dapr.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
{
    public class Query
    {
        private readonly IMediator _mediator;
        private readonly InventoryApi.InventoryApiClient _inventoryApiClient;

        public Query(IMediator mediator, InventoryApi.InventoryApiClient inventoryApiClient)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _inventoryApiClient = inventoryApiClient ?? throw new NullReferenceException(nameof(inventoryApiClient));
        }

        public async Task<IEnumerable<CatalogProductDto>> GetProducts()
        {
            var options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            var client = new DaprClientBuilder()
                .UseEndpoint("http://localhost:5303")
                .UseJsonSerializationOptions(options)
                .Build();
            
            var inventories = await client.InvokeMethodAsync<GetInventoriesRequest, List<InventoryDto>>("inventory-api",
                "GetInventories", new GetInventoriesRequest());

            var result = await _mediator.Send(new GetProductsQuery());
            //var inventoryResponse = await _inventoryApiClient.GetInventoriesAsync(new GetInventoriesRequest());
            //var inventories = inventoryResponse.Inventories;


            return result.ToList().Select(x =>
            {
                var inv = inventories.FirstOrDefault(i => i.Id == x.InventoryId);
                if (inv == null) return x;
                x.InventoryDescription = inv?.Description;
                x.InventoryLocation = inv?.Location;
                x.InventoryWebsite = inv?.Website;
                return x;
            });
        }
    }
}
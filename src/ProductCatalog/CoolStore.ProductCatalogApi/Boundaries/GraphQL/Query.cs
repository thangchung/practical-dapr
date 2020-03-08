using CoolStore.ProductCatalogApi.UseCases.GetProducts;
using CoolStore.Protobuf.Inventory.V1;
using CoolStore.Protobuf.ProductCatalog.V1;
using MediatR;
using N8T.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolStore.ProductCatalogApi.Boundaries.GraphQL
{
    public class Query
    {
        private readonly IMediator _mediator;
        private readonly ServiceOptions _serviceOptions;
        private readonly InventoryApi.InventoryApiClient _inventoryApiClient;

        public Query(IMediator mediator, ServiceOptions serviceOptions, InventoryApi.InventoryApiClient inventoryApiClient)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _serviceOptions = serviceOptions ?? throw new ArgumentNullException(nameof(serviceOptions));
            _inventoryApiClient = inventoryApiClient ?? throw new NullReferenceException(nameof(inventoryApiClient));
        }

        public async Task<IEnumerable<CatalogProductDto>> GetProducts()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            var inventoryResponse = await _inventoryApiClient.GetInventoriesAsync(new GetInventoriesRequest());
            var inventories = inventoryResponse.Inventories;

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
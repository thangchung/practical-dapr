using CoolStore.ProductCatalogApi.Application.UseCase.GetProducts;
using CoolStore.Protobuf.Inventory.V1;
using CoolStore.Protobuf.ProductCatalog.V1;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using N8T.Infrastructure.Dapr;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
{
    public class Query
    {
        private readonly IMediator _mediator;

        public Query(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IEnumerable<CatalogProductDto>> GetProducts()
        {
            var client = "http://localhost:5303".GetDaprClient();

            var inventories = await client.InvokeMethodAsync<GetInventoriesRequest, List<InventoryDto>>(
                "inventory-api",
                "GetInventories",
                new GetInventoriesRequest());

            var result = await _mediator.Send(new GetProductsQuery());

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
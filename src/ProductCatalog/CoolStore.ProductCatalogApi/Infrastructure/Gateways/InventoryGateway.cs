using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.Protobuf.Inventory.V1;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure;
using N8T.Infrastructure.Dapr;

namespace CoolStore.ProductCatalogApi.Infrastructure.Gateways
{
    public class InventoryGateway : IInventoryGateway
    {
        private readonly IConfiguration _config;

        public InventoryGateway(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IReadOnlyDictionary<Guid, InventoryDto>> GetInventoriesAsync(
            IReadOnlyCollection<Guid> invIds,
            CancellationToken cancellationToken)
        {
            var daprClient = _config.GetDaprClient("inventory-api");

            var request = new GetInventoriesByIds();
            request.Ids.AddRange(invIds.Select(x => x.ToString()));

            var inventories = await daprClient.InvokeMethodAsync<GetInventoriesByIds, List<InventoryDto>>(
                "inventory-api",
                "GetInventoriesByIds",
                request,
                null,
                cancellationToken);

            return inventories.ToDictionary(x => x.Id.ConvertTo<Guid>());
        }
    }
}

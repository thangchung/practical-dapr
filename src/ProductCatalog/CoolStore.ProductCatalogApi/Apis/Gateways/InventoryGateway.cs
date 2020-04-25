using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Domain;
using CoolStore.Protobuf.Inventory.V1;
using N8T.Infrastructure;

namespace CoolStore.ProductCatalogApi.Apis.Gateways
{
    public class InventoryGateway : IInventoryGateway
    {
        private readonly InventoryApi.InventoryApiClient _client;

        public InventoryGateway(InventoryApi.InventoryApiClient client)
        {
            _client = client;
        }

        public async Task<IReadOnlyDictionary<Guid, InventoryDto>> GetInventoriesAsync(
            IReadOnlyCollection<Guid> invIds,
            CancellationToken cancellationToken)
        {
            var request = new GetInventoriesByIdsRequest();
            request.Ids.AddRange(invIds.Select(x => x.ToString()));

            var response = await _client.GetInventoriesByIdsAsync(request);
            return response.Inventories.ToDictionary(x => x.Id.ConvertTo<Guid>());
        }
    }
}

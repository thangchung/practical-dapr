using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Domain;
using CoolStore.ProductCatalogApi.Dtos;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure;
using N8T.Infrastructure.Dapr;

namespace CoolStore.ProductCatalogApi.UserInterface.Gateways
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

            var request = new InventoriesByIdsDto();
            request.Ids.AddRange(invIds.Select(x => x.ToString()));

            var inventories = await daprClient.InvokeMethodAsync<InventoriesByIdsDto, List<InventoryDto>>(
                "inventory-api",
                "GetInventoriesByIds",
                request,
                null,
                cancellationToken);

            return inventories.ToDictionary(x => x.Id.ConvertTo<Guid>());
        }
    }
}

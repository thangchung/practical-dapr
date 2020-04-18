using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.Protobuf.Inventory.V1;

namespace CoolStore.ProductCatalogApi.Infrastructure.Gateways
{
    public interface IInventoryGateway
    {
        Task<IReadOnlyDictionary<Guid, InventoryDto>> GetInventoriesAsync(
            IReadOnlyCollection<Guid> invIds,
            CancellationToken cancellationToken);
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Dtos;

namespace CoolStore.ProductCatalogApi.Domain
{
    public interface IInventoryGateway
    {
        Task<IReadOnlyDictionary<Guid, InventoryDto>> GetInventoriesAsync(
            IReadOnlyCollection<Guid> invIds,
            CancellationToken cancellationToken);
    }
}

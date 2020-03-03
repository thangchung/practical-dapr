using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoolStore.Protobuf.Inventory.V1;

namespace CoolStore.ProductCatalogApi.Domain
{
    public interface IInventoryGateway
    {
        Task<InventoryDto> GetInventoryAsync(Guid inventoryId);
        Task<IEnumerable<InventoryDto>> GetAvailabilityInventories();
    }
}
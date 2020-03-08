using CoolStore.Protobuf.Inventory.V1;
using MediatR;
using System.Linq;

namespace CoolStore.InventoryApi.UseCases.GetAvailabilityInventories
{
    public class GetInventoriesQuery : IRequest<IQueryable<InventoryDto>>
    {
    }
}
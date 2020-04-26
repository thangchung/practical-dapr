using System.Linq;
using CoolStore.Protobuf.Inventory.V1;
using MediatR;

namespace CoolStore.InventoryApi.Application.UseCases.GetAvailabilityInventories
{
    public class GetInventoriesQuery : IRequest<IQueryable<InventoryDto>>
    {
    }
}

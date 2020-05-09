using System.Linq;
using CoolStore.Protobuf.Inventory.V1;
using MediatR;

namespace CoolStore.InventoryApi.Application.GetAvailabilityStores
{
    public class GetStoresQuery : IRequest<IQueryable<StoreDto>>
    {
    }
}

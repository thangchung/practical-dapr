using System.Collections.Generic;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Application.GetAvailabilityStores;
using CoolStore.Protobuf.Inventory.V1;
using HotChocolate;
using MediatR;

namespace CoolStore.InventoryApi.Infrastructure.Apis.GraphQL
{
    public class Query
    {
        public async Task<IEnumerable<StoreDto>> GetStores([Service] IMediator mediator)
        {
            return await mediator.Send(new GetStoresQuery());
        }
    }
}

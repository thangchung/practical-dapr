using System.Collections.Generic;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Application.UseCases.GetAvailabilityStores;
using CoolStore.Protobuf.Inventory.V1;
using MediatR;

namespace CoolStore.InventoryApi.Infrastructure.Apis.GraphQL
{
    public class Query
    {
        private readonly IMediator _mediator;

        public Query(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<StoreDto>> GetStores()
        {
            return await _mediator.Send(new GetStoresQuery());
        }
    }
}

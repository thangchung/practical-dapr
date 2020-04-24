using System.Collections.Generic;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Application.UseCases.GetAvailabilityInventories;
using CoolStore.InventoryApi.Dtos;
using MediatR;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class Query
    {
        private readonly IMediator _mediator;

        public Query(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<InventoryDto>> GetInventories()
        {
            return await _mediator.Send(new GetInventoriesQuery());
        }
    }
}

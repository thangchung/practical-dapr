using System;
using System.Linq;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Application.UseCases.GetAvailabilityInventories;
using CoolStore.InventoryApi.Application.UseCases.GetInventory;
using CoolStore.Protobuf.Inventory.V1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using N8T.Infrastructure;

namespace CoolStore.InventoryApi.Apis.Grpc
{
    public class InventoryService : Protobuf.Inventory.V1.InventoryApi.InventoryApiBase
    {
        private readonly IMediator _mediator;

        public InventoryService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override Task<Empty> Ping(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new Empty());
        }

        public override async Task<GetInventoriesResponse> GetInventories(
            GetInventoriesRequest request,
            ServerCallContext context)
        {
            var result = await _mediator.Send(new GetInventoriesQuery());
            var response = new GetInventoriesResponse();
            response.Inventories.AddRange(result.ToList());
            return response;
        }

        public override async Task<GetInventoriesByIdsResponse> GetInventoriesByIds(
            GetInventoriesByIdsRequest request,
            ServerCallContext context)
        {
            var result = await _mediator.Send(
                new GetInventoriesByIdsQuery {Ids = request.Ids.Select(x => x.ConvertTo<Guid>())});
            var response = new GetInventoriesByIdsResponse();
            response.Inventories.AddRange(result.ToList());
            return response;
        }
    }
}

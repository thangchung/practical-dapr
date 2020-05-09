using System;
using System.Linq;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Application.GetAvailabilityStores;
using CoolStore.InventoryApi.Application.GetStoresByIds;
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

        public override async Task<GetStoresResponse> GetStores(
            GetStoresRequest request,
            ServerCallContext context)
        {
            var result = await _mediator.Send(new GetStoresQuery());
            var response = new GetStoresResponse();
            response.Stores.AddRange(result.ToList());
            return response;
        }

        public override async Task<GetStoresByIdsResponse> GetStoresByIds(
            GetStoresByIdsRequest request,
            ServerCallContext context)
        {
            var result = await _mediator.Send(
                new GetStoresByIdsQuery {Ids = request.Ids.Select(x => x.ConvertTo<Guid>())});
            var response = new GetStoresByIdsResponse();
            response.Stores.AddRange(result.ToList());
            return response;
        }
    }
}

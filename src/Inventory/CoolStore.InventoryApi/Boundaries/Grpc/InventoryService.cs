using System;
using System.Threading.Tasks;
using CoolStore.InventoryApi.UseCases.GetAvailabilityInventories;
using CoolStore.InventoryApi.UseCases.GetInventory;
using CoolStore.Protobuf.Inventory.V1;
using Grpc.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure;

namespace CoolStore.InventoryApi.Boundaries.Grpc
{
    public class InventoryService : Protobuf.Inventory.V1.InventoryApi.InventoryApiBase
    {
        private readonly IMediator _mediator;

        public InventoryService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override async Task<GetInventoriesResponse> GetInventories(GetInventoriesRequest request,
            ServerCallContext context)
        {
            var result = await _mediator.Send(new GetInventoriesQuery());
            var response = new GetInventoriesResponse();
            response.Inventories.AddRange(await result.ToListAsync());
            return response;
        }

        public override async Task<GetInventoryResponse> GetInventory(GetInventoryRequest request,
            ServerCallContext context)
        {
            var result = await _mediator.Send(new GetInventoryQuery {Id = request.Id.ConvertTo<Guid>()});
            return new GetInventoryResponse
            {
                Result = result
            };
        }

        public override Task GetInventoryAsyncStream(GetInventoryStreamRequest request,
            IServerStreamWriter<GetInventoryStreamResponse> responseStream,
            ServerCallContext context)
        {
            return base.GetInventoryAsyncStream(request, responseStream, context);
        }
    }
}
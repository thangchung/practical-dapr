using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Application.UseCases.GetAvailabilityInventories;
using CoolStore.InventoryApi.Application.UseCases.GetInventory;
using CoolStore.Protobuf.Inventory.V1;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using N8T.Infrastructure.Grpc;
using CoolStoreDapr = CoolStore.Dapr.Client.Autogen.Grpc;

namespace CoolStore.InventoryApi.UserInterface.Grpc
{
    public class DaprService : CoolStoreDapr.Dapr.DaprBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DaprService> _logger;

        public DaprService(
            IMediator mediator,
            ILogger<DaprService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task<CoolStoreDapr.InvokeServiceResponseEnvelope> InvokeService(
            CoolStoreDapr.InvokeServiceEnvelope request,
            ServerCallContext context)
        {
            _logger.LogInformation($"Id: {request.Id}, method: {request.Method}");

            var responseEnvelope = new CoolStoreDapr.InvokeServiceResponseEnvelope();

            switch (request.Method)
            {
                case "GetInventories":
                {
                    var result = await _mediator.Send(new GetInventoriesQuery());
                    _logger.LogInformation($"Got {result.ToList().Count} items.");
                    var inventories = new List<InventoryDto>();
                    inventories.AddRange(result);
                    responseEnvelope.Data = inventories.ConvertToAnyAsync();
                    return responseEnvelope;
                }

                case "GetInventory":
                {
                    var result = await _mediator.Send(new GetInventoryQuery());
                    responseEnvelope.Data = result.ConvertToAnyAsync();
                    return responseEnvelope;
                }
            }

            return responseEnvelope;
        }
    }
}

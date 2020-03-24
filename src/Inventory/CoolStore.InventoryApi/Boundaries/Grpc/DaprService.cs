using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CoolStore.InventoryApi.UseCases.GetAvailabilityInventories;
using CoolStore.Protobuf.Inventory.V1;
using Dapr.Client.Autogen.Grpc;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoolStore.InventoryApi.Boundaries.Grpc
{
    public class DaprService : Dapr.Client.Autogen.Grpc.Dapr.DaprBase
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

        public override async Task<InvokeServiceResponseEnvelope> InvokeService(InvokeServiceEnvelope request, ServerCallContext context)
        {
            _logger.LogInformation($"Id: {request.Id}, method: {request.Method}");

            var responseEnvelope = new InvokeServiceResponseEnvelope();

            switch (request.Method)
            {
                case "GetInventories":
                {
                    var result = await _mediator.Send(new GetInventoriesQuery());
                    _logger.LogInformation($"Got {result.ToList().Count} items.");
                    var inventories = new List<InventoryDto>();
                    inventories.AddRange(result);
                    responseEnvelope.Data = ConvertToAnyAsync(inventories);
                    return responseEnvelope;
                }
            }

            return responseEnvelope;
        }

        private static Any ConvertToAnyAsync<T>(T data, JsonSerializerOptions options = null)
        {
            options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            var any = new Any();

            if (data == null) return any;
            var bytes = JsonSerializer.SerializeToUtf8Bytes(data, options);
            any.Value = ByteString.CopyFrom(bytes);

            return any;
        }
    }
}
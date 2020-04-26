using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Domain;
using CoolStore.Protobuf.Inventory.V1;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using N8T.Infrastructure;

namespace CoolStore.ProductCatalogApi.Apis.Gateways
{
    public class InventoryGateway : IInventoryGateway
    {
        private readonly InventoryApi.InventoryApiClient _client;
        private readonly IHttpContextAccessor _httpContext;

        public InventoryGateway(InventoryApi.InventoryApiClient client, IHttpContextAccessor httpContext)
        {
            _client = client;
            _httpContext = httpContext;
        }

        public async Task<IReadOnlyDictionary<Guid, InventoryDto>> GetInventoriesAsync(
            IReadOnlyCollection<Guid> invIds,
            CancellationToken cancellationToken)
        {
            var activity = new Activity("call to inventory-api").Start();

            var headers = _httpContext.HttpContext.Request.Headers;

            try
            {
                var request = new GetInventoriesByIdsRequest();
                request.Ids.AddRange(invIds.Select(x => x.ToString()));

                var metadata = new Metadata();
                if (headers.TryGetValue("traceparent", out var traceparent))
                {
                    metadata.Add("traceparent", traceparent);
                }

                var response = await _client.GetInventoriesByIdsAsync(request, metadata);
                return response.Inventories.ToDictionary(x => x.Id.ConvertTo<Guid>());
            }
            finally
            {
                activity.Stop();
            }
        }
    }
}

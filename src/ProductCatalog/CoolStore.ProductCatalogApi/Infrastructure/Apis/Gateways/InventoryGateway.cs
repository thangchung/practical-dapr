using System;
using System.Collections.Generic;
using System.Linq;
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

        public InventoryGateway(
            InventoryApi.InventoryApiClient client,
            IHttpContextAccessor httpContext)
        {
            _client = client;
            _httpContext = httpContext;
        }

        public async Task<IReadOnlyDictionary<Guid, StoreDto>> GetStoresAsync(
            IReadOnlyCollection<Guid> storeIds,
            CancellationToken cancellationToken)
        {
            var headers = _httpContext.HttpContext.Request.Headers;
            var metadata = new Metadata();

            // propagate headers to grpc headers
            _ = headers.Select(h =>
            {
                metadata.Add(new Metadata.Entry(h.Key, h.Value));
                return h;
            }).ToList();

            var request = new GetStoresByIdsRequest();
            request.Ids.AddRange(storeIds.Select(x => x.ToString()));

            var response = await _client.GetStoresByIdsAsync(
                request,
                metadata,
                DateTime.UtcNow + TimeSpan.FromSeconds(10));

            return response.Stores.ToDictionary(x => x.Id.ConvertTo<Guid>());
        }
    }
}

using System;
using CoolStore.ProductCatalogApi.Application.UseCase.GetProducts;
using CoolStore.Protobuf.Inventory.V1;
using CoolStore.Protobuf.ProductCatalog.V1;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
{
    public class Query
    {
        private readonly IMediator _mediator;
        private readonly DaprClient _dapr;
        private readonly IConfiguration _config;
        private readonly ILogger<Query> _logger;

        public Query(IMediator mediator
            , DaprClient dapr
            , IConfiguration config
            , ILogger<Query> logger
            )
        {
            _mediator = mediator;
            _dapr = dapr;
            _config = config;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogProductDto>> GetProducts()
        {
            // Add the verb to metadata if the method is other than a POST
            var metaData = new Dictionary<string, string>();
            metaData.Add("http.verb", "GET");
            
            var inventories = await _dapr.InvokeMethodAsync<List<InventoryDto>>(
                "inventory-api",
                "stores",
                metadata: metaData);

            var result = await _mediator.Send(new GetProductsQuery());

            return result.ToList().Select(x =>
            {
                var inv = inventories.FirstOrDefault(i => i.Id == x.InventoryId);
                if (inv == null) return x;
                x.InventoryDescription = inv?.Description;
                x.InventoryLocation = inv?.Location;
                x.InventoryWebsite = inv?.Website;
                return x;
            });
        }

        public static Uri? GetServiceUri(IConfiguration configuration, string name)
        {
            var host = configuration[$"service:{name}:host"];
            var port = configuration[$"service:{name}:port"];
            var protocol = configuration[$"service:{name}:protocol"] ?? "http";

            if (string.IsNullOrEmpty(host) || port == null)
            {
                return null;
            }

            return new Uri(protocol + "://" + host + ":" + port + "/");
        }
    }
}

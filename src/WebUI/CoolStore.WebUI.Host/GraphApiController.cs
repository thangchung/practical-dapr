using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoolStore.WebUI.Host
{
    public class GraphApiController : Controller
    {
        private readonly IGraphQLClient _client;

        public GraphApiController(IGraphQLClient client)
        {
            _client = client;
        }
        
        [HttpGet("api/products/count")]
        public async Task<int?> GetProductCount()
        {
            var result = await _client.GetProductsAsync();
            return result.Data?.Products?.TotalCount;
        }

        [Authorize]
        [HttpGet("api/products")]
        public async Task<ImmutableList<ICatalogProductDto>> GetProducts()
        {
            var result = await _client.GetProductsAsync();
            return result.Data?.Products?.Edges?.ToImmutableList();
        }
    }
}

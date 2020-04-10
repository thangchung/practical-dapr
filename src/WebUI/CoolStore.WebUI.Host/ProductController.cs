using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoolStore.WebUI.Host
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IGraphQLClient _client;

        public ProductController(IGraphQLClient client)
        {
            _client = client;
        }

        [HttpGet("count")]
        public async Task<int?> GetProductCount()
        {
            var result = await _client.GetProductsAsync();
            return result.Data?.Products?.TotalCount;
        }

        [Authorize]
        [HttpGet]
        public async Task<ImmutableList<ICatalogProductDto>> GetProducts()
        {
            var result = await _client.GetProductsAsync();
            return result.Data?.Products?.Edges?.ToImmutableList();
        }
    }
}

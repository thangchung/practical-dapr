using System.Collections.Immutable;
using System.Threading.Tasks;
using CoolStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrawberryShake;

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

        [Authorize]
        [HttpPost]
        public async Task<ICreateProductResponse> CreateProduct([FromBody] CreateProductModel model)
        {
            var result = await _client.CreateProductMutationAsync(new Optional<CreateProductInput>(new CreateProductInput
            {
                Name = model.Name,
                Price = model.Price,
                InventoryId = model.InventoryId,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                Description = model.Description
            }));

            return result.Data.CreateProduct;
        }
    }
}

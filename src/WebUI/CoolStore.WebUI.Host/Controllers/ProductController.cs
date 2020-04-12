using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using CoolStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure;
using StrawberryShake;

namespace CoolStore.WebUI.Host.Controllers
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
        [HttpGet("{id}")]
        public async Task<ICatalogProductDto> GetProductCount(Guid id)
        {
            var result = await _client.GetProductsAsync(1, int.MaxValue);
            return result.Data?.Products?.Edges?.FirstOrDefault(x=>x.Id.ConvertTo<Guid>() == id);
        }

        [Authorize]
        [HttpGet("{page}/{pageSize}/{filter}")]
        public async Task<IOffsetPagingOfCatalogProductDto> GetProducts(int page = 1, int pageSize = 20, string filter = "")
        {
            filter = filter.Replace("&", "").Trim();
            var filterObject = new CatalogProductDtoFilter();
            if (!string.IsNullOrEmpty(filter))
            {
                filterObject.NameContains = filter;
                var filterObjectOptional = new Optional<CatalogProductDtoFilter>(filterObject);
                var result = await _client.GetProductsAsync(page, pageSize, filterObjectOptional);
                return result.Data?.Products;
            }
            else
            {
                var result = await _client.GetProductsAsync(page, pageSize);
                return result.Data?.Products;
            }
        }

        [Authorize]
        [HttpGet("categories")]
        public Task<ImmutableList<KeyValueModel>> GetCategories()
        {
            var result = new List<KeyValueModel>
            {
                new KeyValueModel {Key = new Guid("77666AA8-682C-4047-B075-04839281630A"), Value = "Beverage products"}
            };

            return Task.FromResult(result.ToImmutableList());
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

        [Authorize]
        [HttpPut]
        public async Task<ICreateProductResponse> EditProduct([FromBody] EditProductModel model)
        {
            throw new NotImplementedException();
        }
    }
}

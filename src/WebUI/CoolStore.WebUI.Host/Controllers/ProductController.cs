using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using CoolStore.WebUI.Components;
using CoolStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ProductItemDetailModel> GetProduct(Guid id)
        {
            var result = await _client.GetProductsAsync(1, int.MaxValue);
            var product = result.Data?.Products?.Edges?.FirstOrDefault(x => x.Id == id);
            return new ProductItemDetailModel
            {
                ProductId = product.Id,
                Name = product.Name,
                InventoryId = product.Inventory.Id,
                InventoryLocation = product.Inventory.Location,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Description = product.Description
            };
        }

        [Authorize]
        [HttpGet("{page}/{pageSize}/{filter}")]
        public async Task<ProductModel> GetProducts(int page = 1, int pageSize = 20, string filter = "")
        {
            filter = filter.Replace("&", "").Trim();
            var filterObject = new CatalogProductDtoFilter();
            if (!string.IsNullOrEmpty(filter))
            {
                filterObject.NameContains = filter;
                var filterObjectOptional = new Optional<CatalogProductDtoFilter>(filterObject);
                var result = await _client.GetProductsAsync(page, pageSize, filterObjectOptional);

                var model = new ProductModel {TotalCount = result.Data.Products.TotalCount};
                model.Items.AddRange(result.Data.Products.Edges.Select(x => new ProductItemModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    CategoryId = x.Category.Id,
                    CategoryName = x.Category.Name,
                    InventoryId = x.Inventory.Id,
                    InventoryLocation = x.Inventory.Location
                }));

                return model;
            }
            else
            {
                var result = await _client.GetProductsAsync(page, pageSize);

                var model = new ProductModel {TotalCount = result.Data.Products.TotalCount};
                model.Items.AddRange(result.Data.Products.Edges.Select(x => new ProductItemModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    CategoryId = x.Category.Id,
                    CategoryName = x.Category.Name,
                    InventoryId = x.Inventory.Id,
                    InventoryLocation = x.Inventory.Location
                }));

                return model;
            }
        }

        [Authorize]
        [HttpGet("categories")]
        public async Task<ImmutableList<KeyValueModel>> GetCategories()
        {
            var response = await _client.GetCategoriesAsync();
            return response.Data.Categories
                .Select(x => new KeyValueModel {Key = x.Id.ToString(), Value = x.Name})
                .ToImmutableList();
        }

        [Authorize]
        [HttpPost]
        public async Task CreateProduct([FromBody] CreateProductModel model)
        {
            await _client.CreateProductMutationAsync(new Optional<CreateProductInput>(new CreateProductInput
            {
                Name = model.Name,
                Price = model.Price,
                InventoryId = model.InventoryId.ConvertTo<Guid>(),
                CategoryId = model.CategoryId.ConvertTo<Guid>(),
                ImageUrl = model.ImageUrl,
                Description = model.Description
            }));
        }

        [Authorize]
        [HttpPut]
        public async Task EditProduct([FromBody] EditProductModel model)
        {
            await _client.UpdateProductMutationAsync(new Optional<UpdateProductInput>(new UpdateProductInput
            {
                Name = model.Name,
                Price = model.Price,
                InventoryId = model.InventoryId.ConvertTo<Guid>(),
                CategoryId = model.CategoryId.ConvertTo<Guid>(),
                ImageUrl = model.ImageUrl,
                Description = model.Description
            }));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task DeleteProduct(string id)
        {
            await _client.DeleteProductMutationAsync(new Optional<DeleteProductInput>(new DeleteProductInput
            {
                Id = id.ConvertTo<Guid>()
            }));
        }
    }
}

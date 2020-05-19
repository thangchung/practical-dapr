using System.Collections.Generic;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Application.GetCategories;
using CoolStore.ProductCatalogApi.Application.GetProducts;
using CoolStore.ProductCatalogApi.Dtos;
using HotChocolate;
using MediatR;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class Query
    {
        public async Task<IEnumerable<CatalogProductDto>> GetProducts([Service] IMediator mediator)
        {
            return await mediator.Send(new GetProductsQuery());
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories([Service] IMediator mediator)
        {
            return await mediator.Send(new GetCategoriesQuery());
        }
    }
}

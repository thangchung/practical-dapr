using System.Collections.Generic;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Application.UseCases.GetCategories;
using CoolStore.ProductCatalogApi.Application.UseCases.GetProducts;
using CoolStore.ProductCatalogApi.Dtos;
using MediatR;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class Query
    {
        private readonly IMediator _mediator;

        public Query(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<CatalogProductDto>> GetProducts()
        {
            return await _mediator.Send(new GetProductsQuery());
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            return await _mediator.Send(new GetCategoriesQuery());
        }
    }
}

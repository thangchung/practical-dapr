using System.Collections.Generic;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Application.UseCase.GetProducts;
using CoolStore.ProductCatalogApi.Dtos;
using MediatR;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
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
    }
}

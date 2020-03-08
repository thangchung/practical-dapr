using System.Collections.Generic;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.UseCases.GetProductsByPriceAndName;
using CoolStore.Protobuf.ProductCatalog.V1;
using MediatR;

namespace CoolStore.ProductCatalogApi.Boundaries.GraphQL
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
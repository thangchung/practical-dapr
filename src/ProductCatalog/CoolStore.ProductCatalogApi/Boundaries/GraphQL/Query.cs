using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var response = await _mediator.Send(new GetProductsRequest());
            return response.Products.ToList();
        }
    }
}
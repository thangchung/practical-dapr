using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Application.UseCases.CreateProduct;
using CoolStore.ProductCatalogApi.Dtos;
using MediatR;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class Mutation
    {
        private readonly IMediator _mediator;

        public Mutation(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CatalogProductDto> CreateProduct(CreateProductRequest createProductInput)
        {
            return await _mediator.Send(createProductInput);
        }
    }
}

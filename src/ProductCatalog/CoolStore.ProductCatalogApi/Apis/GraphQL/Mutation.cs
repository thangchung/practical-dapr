using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Application.UseCases.CreateProduct;
using CoolStore.ProductCatalogApi.Application.UseCases.DeleteProduct;
using CoolStore.ProductCatalogApi.Application.UseCases.UpdateProduct;
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

        public async Task<CatalogProductDto> CreateProduct(CreateProductCommand createProductInput)
        {
            return await _mediator.Send(createProductInput);
        }

        public async Task<CatalogProductDto> UpdateProduct(UpdateProductCommand updateProductInput)
        {
            return await _mediator.Send(updateProductInput);
        }

        public async Task<bool> DeleteProduct(DeleteProductCommand deleteProductInput)
        {
            return await _mediator.Send(deleteProductInput);
        }
    }
}

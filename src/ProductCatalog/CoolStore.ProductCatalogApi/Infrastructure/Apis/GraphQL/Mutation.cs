using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Application.CreateProduct;
using CoolStore.ProductCatalogApi.Application.DeleteProduct;
using CoolStore.ProductCatalogApi.Application.UpdateProduct;
using CoolStore.ProductCatalogApi.Dtos;
using HotChocolate;
using MediatR;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class Mutation
    {
        public async Task<CatalogProductDto> CreateProduct(
            CreateProductCommand createProductInput,
            [Service] IMediator mediator)
        {
            return await mediator.Send(createProductInput);
        }

        public async Task<CatalogProductDto> UpdateProduct(
            UpdateProductCommand updateProductInput,
            [Service] IMediator mediator)
        {
            return await mediator.Send(updateProductInput);
        }

        public async Task<bool> DeleteProduct(
            DeleteProductCommand deleteProductInput,
            [Service] IMediator mediator)
        {
            return await mediator.Send(deleteProductInput);
        }
    }
}

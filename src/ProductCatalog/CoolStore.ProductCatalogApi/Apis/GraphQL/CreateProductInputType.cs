using CoolStore.ProductCatalogApi.Application.UseCases.CreateProduct;
using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class CreateProductInputType : InputObjectType<CreateProductRequest>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreateProductRequest> descriptor)
        {
            descriptor.Name("CreateProductInput");
        }
    }
}

using CoolStore.ProductCatalogApi.Application.UseCases.CreateProduct;
using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class CreateProductInputType : InputObjectType<CreateProductCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreateProductCommand> descriptor)
        {
            descriptor.Name("CreateProductInput");
        }
    }
}

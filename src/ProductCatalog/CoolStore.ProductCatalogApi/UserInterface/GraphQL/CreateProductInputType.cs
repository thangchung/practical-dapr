using CoolStore.ProductCatalogApi.Application.UseCase.CreateProduct;
using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
{
    public class CreateProductInputType : InputObjectType<CreateProductRequest>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreateProductRequest> descriptor)
        {
            descriptor.Name("CreateProductInput");
        }
    }
}

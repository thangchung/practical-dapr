using CoolStore.ProductCatalogApi.Application.UseCases.UpdateProduct;
using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class UpdateProductInputType : InputObjectType<UpdateProductCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdateProductCommand> descriptor)
        {
            descriptor.Name("UpdateProductInput");
        }
    }
}

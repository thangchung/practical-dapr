using CoolStore.ProductCatalogApi.Application.UseCases.DeleteProduct;
using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class DeleteProductInputType : InputObjectType<DeleteProductCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<DeleteProductCommand> descriptor)
        {
            descriptor.Name("DeleteProductInput");
        }
    }
}

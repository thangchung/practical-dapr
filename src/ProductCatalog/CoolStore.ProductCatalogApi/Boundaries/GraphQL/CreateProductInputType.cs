using CoolStore.Protobuf.ProductCatalog.V1;
using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.Boundaries.GraphQL
{
    public class CreateProductInputType : InputObjectType<CreateProductRequest>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreateProductRequest> descriptor)
        {
            descriptor.Name("CreateProductInput");
        }
    }
}
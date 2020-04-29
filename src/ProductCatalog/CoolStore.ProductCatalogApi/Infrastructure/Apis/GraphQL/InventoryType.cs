using HotChocolate.Types;
using N8T.Infrastructure.GraphQL;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class InventoryType : ProtoObjectType<Protobuf.Inventory.V1.InventoryDto>
    {
        protected override void Configure(IObjectTypeDescriptor<Protobuf.Inventory.V1.InventoryDto> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();

            base.Configure(descriptor);
        }
    }
}

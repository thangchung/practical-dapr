using CoolStore.Protobuf.Inventory.V1;
using HotChocolate.Types;
using N8T.Infrastructure.GraphQL;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
{
    public class InventoryType : ProtoObjectType<InventoryDto>
    {
        protected override void Configure(IObjectTypeDescriptor<InventoryDto> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();

            base.Configure(descriptor);
        }
    }
}

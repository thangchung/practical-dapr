using CoolStore.Protobuf.Inventory.V1;
using HotChocolate.Types;

namespace CoolStore.InventoryApi.Infrastructure.Apis.GraphQL
{
    public class InventoryType : ObjectType<InventoryDto>
    {
        protected override void Configure(IObjectTypeDescriptor<InventoryDto> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();

            base.Configure(descriptor);
        }
    }
}

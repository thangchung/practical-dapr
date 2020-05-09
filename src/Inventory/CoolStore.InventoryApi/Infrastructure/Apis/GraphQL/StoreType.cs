using CoolStore.Protobuf.Inventory.V1;
using HotChocolate.Types;

namespace CoolStore.InventoryApi.Infrastructure.Apis.GraphQL
{
    public class StoreType : ObjectType<StoreDto>
    {
        protected override void Configure(IObjectTypeDescriptor<StoreDto> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();

            base.Configure(descriptor);
        }
    }
}

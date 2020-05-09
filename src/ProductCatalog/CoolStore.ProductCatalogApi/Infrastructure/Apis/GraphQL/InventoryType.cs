using CoolStore.Protobuf.Inventory.V1;
using HotChocolate.Types;
using N8T.Infrastructure.GraphQL;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class InventoryType : ProtoObjectType<StoreDto>
    {
        protected override void Configure(IObjectTypeDescriptor<StoreDto> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();

            base.Configure(descriptor);
        }
    }
}

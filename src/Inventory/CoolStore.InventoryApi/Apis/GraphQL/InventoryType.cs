using CoolStore.InventoryApi.Dtos;
using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
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

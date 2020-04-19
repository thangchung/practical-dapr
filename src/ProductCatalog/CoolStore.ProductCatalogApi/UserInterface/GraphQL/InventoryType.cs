using CoolStore.ProductCatalogApi.Dtos;
using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
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

using CoolStore.ProductCatalogApi.Dtos;
using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
{
    public class CategoryType : ObjectType<CategoryDto>
    {
        protected override void Configure(IObjectTypeDescriptor<CategoryDto> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();

            base.Configure(descriptor);
        }
    }
}

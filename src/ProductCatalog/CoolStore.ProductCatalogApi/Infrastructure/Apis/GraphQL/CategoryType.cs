using CoolStore.ProductCatalogApi.Dtos;
using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
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

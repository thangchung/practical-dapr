using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
{
    public sealed class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field(t => t.CreateProduct(default))
                .Type<NonNullType<CatalogProductType>>()
                .Argument("createProductInput", a => a
                    .Type<NonNullType<CreateProductInputType>>());
        }
    }
}

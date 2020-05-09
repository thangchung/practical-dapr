using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public sealed class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field(t => t.CreateProduct(default!, default!))
                .Type<NonNullType<CatalogProductType>>()
                .Argument("createProductInput", a => a
                    .Type<NonNullType<CreateProductInputType>>());

            descriptor.Field(t => t.UpdateProduct(default!, default!))
                .Type<NonNullType<CatalogProductType>>()
                .Argument("updateProductInput", a => a
                    .Type<NonNullType<UpdateProductInputType>>());

            descriptor.Field(t => t.DeleteProduct(default!, default!))
                .Type<NonNullType<BooleanType>>()
                .Argument("deleteProductInput", a => a
                    .Type<NonNullType<DeleteProductInputType>>());
        }
    }
}

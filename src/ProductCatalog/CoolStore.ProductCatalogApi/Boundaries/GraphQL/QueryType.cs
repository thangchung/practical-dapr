using HotChocolate.Types;

namespace CoolStore.ProductCatalogApi.Boundaries.GraphQL
{
    public sealed class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor
                .Field(x => x.GetProducts(default, default))
                .Name("products");
        }
    }
}
using CoolStore.ProductCatalogApi.Boundaries.GraphQL.Filters;
using CoolStore.ProductCatalogApi.Boundaries.GraphQL.Sorts;
using CoolStore.Protobuf.ProductCatalog.V1;
using HotChocolate.Types;
using N8T.Infrastructure.GraphQL.OffsetPaging;

namespace CoolStore.ProductCatalogApi.Boundaries.GraphQL
{
    public sealed class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor
                .Field(x => x.GetProducts())
                .Name("products")
                .UseOffsetPaging<ProductType, CatalogProductDto>()
                .UseFiltering<ProductFilterType>()
                .UseSorting<ProductSortType>();
        }
    }
}
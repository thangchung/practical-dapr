using CoolStore.ProductCatalogApi.Apis.GraphQL.Filters;
using CoolStore.ProductCatalogApi.Apis.GraphQL.Sorts;
using CoolStore.ProductCatalogApi.Dtos;
using HotChocolate.Types;
using N8T.Infrastructure.GraphQL.OffsetPaging;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public sealed class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor
                .Field(x => x.GetProducts())
                .Name("products")
                .UseOffsetPaging<CatalogProductType, CatalogProductDto>()
                .UseFiltering<ProductFilterType>()
                .UseSorting<ProductSortType>();

            descriptor
                .Field(x => x.GetCategories())
                .Name("categories");
        }
    }
}

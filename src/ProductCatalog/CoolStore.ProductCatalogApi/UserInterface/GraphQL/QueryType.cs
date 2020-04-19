using CoolStore.ProductCatalogApi.Dtos;
using CoolStore.ProductCatalogApi.UserInterface.GraphQL.Filters;
using CoolStore.ProductCatalogApi.UserInterface.GraphQL.Sorts;
using HotChocolate.Types;
using N8T.Infrastructure.GraphQL.OffsetPaging;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
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
        }
    }
}

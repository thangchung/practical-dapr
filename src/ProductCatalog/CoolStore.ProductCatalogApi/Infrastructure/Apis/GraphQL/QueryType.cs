using CoolStore.ProductCatalogApi.Apis.GraphQL.Filters;
using CoolStore.ProductCatalogApi.Apis.GraphQL.Sorts;
using CoolStore.ProductCatalogApi.Application.GetCategories;
using CoolStore.ProductCatalogApi.Application.GetProducts;
using CoolStore.ProductCatalogApi.Dtos;
using HotChocolate.Types;
using MediatR;
using N8T.Infrastructure.GraphQL;
using N8T.Infrastructure.GraphQL.OffsetPaging;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public sealed class QueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("products")
                .Type<OffsetPagingType<CatalogProductType, CatalogProductDto>>()
                .AddPagingArguments()
                .AddSortingArguments<ProductSortType>()
                .AddFilterArguments<ProductFilterType>()
                .Resolver(async context =>
                    await context.Service<IMediator>()!.Send(new GetProductsQuery().ExtractParams(context)));

            descriptor.Field("categories")
                .Type<ListType<CategoryType>>()
                .Resolver(async context => await context.Service<IMediator>()!.Send(new GetCategoriesQuery()));
        }
    }
}

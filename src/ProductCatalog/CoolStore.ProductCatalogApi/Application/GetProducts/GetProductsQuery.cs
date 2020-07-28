using CoolStore.ProductCatalogApi.Dtos;
using MediatR;
using N8T.Infrastructure.GraphQL;
using N8T.Infrastructure.GraphQL.OffsetPaging;

namespace CoolStore.ProductCatalogApi.Application.GetProducts
{
    public class GetProductsQuery : GraphQueryBase<CatalogProductDto>, IRequest<OffsetPaging<CatalogProductDto>>
    {
    }
}

using System.Linq;
using CoolStore.Protobuf.ProductCatalog.V1;
using MediatR;

namespace CoolStore.ProductCatalogApi.UseCases.GetProductsByPriceAndName
{
    public class GetProductsQuery : IRequest<IQueryable<CatalogProductDto>>
    {
    }
}
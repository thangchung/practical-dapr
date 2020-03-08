using System.Linq;
using CoolStore.Protobuf.ProductCatalog.V1;
using MediatR;

namespace CoolStore.ProductCatalogApi.UseCases.GetProducts
{
    public class GetProductsQuery : IRequest<IQueryable<CatalogProductDto>>
    {
    }
}
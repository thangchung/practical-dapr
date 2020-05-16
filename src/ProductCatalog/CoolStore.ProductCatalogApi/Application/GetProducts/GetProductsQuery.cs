using System.Linq;
using CoolStore.ProductCatalogApi.Dtos;
using MediatR;

namespace CoolStore.ProductCatalogApi.Application.GetProducts
{
    public class GetProductsQuery : IRequest<IQueryable<CatalogProductDto>>
    {
    }
}

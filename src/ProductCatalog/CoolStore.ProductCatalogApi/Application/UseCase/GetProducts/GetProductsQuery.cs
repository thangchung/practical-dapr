using System.Linq;
using CoolStore.ProductCatalogApi.Dtos;
using MediatR;

namespace CoolStore.ProductCatalogApi.Application.UseCase.GetProducts
{
    public class GetProductsQuery : IRequest<IQueryable<CatalogProductDto>>
    {
    }
}

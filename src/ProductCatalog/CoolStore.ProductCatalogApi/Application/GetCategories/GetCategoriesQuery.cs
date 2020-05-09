using System.Linq;
using CoolStore.ProductCatalogApi.Dtos;
using MediatR;

namespace CoolStore.ProductCatalogApi.Application.GetCategories
{
    public class GetCategoriesQuery : IRequest<IQueryable<CategoryDto>>
    {
    }
}

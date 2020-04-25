using System.Linq;
using CoolStore.ProductCatalogApi.Dtos;
using MediatR;

namespace CoolStore.ProductCatalogApi.Application.UseCases.GetCategories
{
    public class GetCategoriesQuery: IRequest<IQueryable<CategoryDto>>
    {
    }
}

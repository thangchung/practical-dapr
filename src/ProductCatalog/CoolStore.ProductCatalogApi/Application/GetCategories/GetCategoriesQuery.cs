using System.Linq;
using CoolStore.ProductCatalogApi.Dtos;
using MediatR;
using N8T.Infrastructure.Auth;

namespace CoolStore.ProductCatalogApi.Application.GetCategories
{
    public class GetCategoriesQuery : IRequest<IQueryable<CategoryDto>>, IAuthRequest
    {
    }
}

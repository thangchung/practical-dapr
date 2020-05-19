using System.Linq;
using CoolStore.ProductCatalogApi.Dtos;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoolStore.ProductCatalogApi.Application.GetCategories
{
    public class GetCategoriesHandler : RequestHandler<GetCategoriesQuery, IQueryable<CategoryDto>>
    {
        private readonly ProductCatalogDbContext _dbContext;

        public GetCategoriesHandler(ProductCatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override IQueryable<CategoryDto> Handle(GetCategoriesQuery request)
        {
            if (request is null)
            {
                throw new System.ArgumentNullException(nameof(request));
            }

            return _dbContext.Categories
                .AsNoTracking()
                .Select(x => new CategoryDto { Id = x.Id, Name = x.Name });
        }
    }
}

using System;
using System.Linq;
using CoolStore.ProductCatalogApi.Dtos;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoolStore.ProductCatalogApi.Application.UseCases.GetProducts
{
    public class GetProductsHandler : RequestHandler<GetProductsQuery, IQueryable<CatalogProductDto>>
    {
        private readonly ProductCatalogDbContext _dbContext;

        public GetProductsHandler(ProductCatalogDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override IQueryable<CatalogProductDto> Handle(GetProductsQuery request)
        {
            return _dbContext.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Where(x => !x.IsDeleted)
                .Select(x => new CatalogProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Category = new CategoryDto {Id = x.Category.Id, Name = x.Category.Name},
                    InventoryId = x.InventoryId
                });
        }
    }
}

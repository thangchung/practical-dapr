using System;
using System.Linq;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using CoolStore.Protobuf.ProductCatalog.V1;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoolStore.ProductCatalogApi.Application.UseCase.GetProducts
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
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    CategoryId = x.Category.Id.ToString(),
                    CategoryName = x.Category.Name,
                    InventoryId = x.InventoryId.ToString()
                });
        }
    }
}
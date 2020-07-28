using System.Linq;
using CoolStore.ProductCatalogApi.Dtos;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure.GraphQL.OffsetPaging;

namespace CoolStore.ProductCatalogApi.Application.GetProducts
{
    public class GetProductsHandler : RequestHandler<GetProductsQuery, OffsetPaging<CatalogProductDto>>
    {
        private readonly ProductCatalogDbContext _dbContext;

        public GetProductsHandler(ProductCatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override OffsetPaging<CatalogProductDto> Handle(GetProductsQuery request)
        {
            if (request is null)
            {
                throw new System.ArgumentNullException(nameof(request));
            }


            var dtoQueryable = _dbContext.Products
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
                    StoreId = x.StoreId
                });

            // filter by GraphQL
            if (request.FilterExpr != null)
            {
                dtoQueryable = dtoQueryable.Where(request.FilterExpr);
            }

            // order_by by GraphQL 
            if (request.SortingVisitor != null)
            {
                dtoQueryable = request.SortingVisitor.Sort(dtoQueryable);
            }

            // pagination
            var totalCount = dtoQueryable.Count();
            dtoQueryable = dtoQueryable.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            // build pagination model
            var model = new OffsetPaging<CatalogProductDto> {TotalCount = totalCount, Edges = dtoQueryable.ToList()};
            return model;
        }
    }
}

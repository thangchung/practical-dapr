using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Persistence;
using CoolStore.Protobuf.ProductCatalog.V1;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoolStore.ProductCatalogApi.UseCases.GetProductsByPriceAndName
{
    public class GetProductsByPriceAndNameHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
    {
        private readonly ProductCatalogDbContext _dbContext;

        public GetProductsByPriceAndNameHandler(ProductCatalogDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Where(x => x.Price <= request.HighPrice && !x.IsDeleted)
                .Skip(request.CurrentPage - 1)
                .Take(10)
                .Select(x => new CatalogProductDto
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    CategoryId = x.Category.Id.ToString(),
                    CategoryName = x.Category.Name,
                })
                .ToListAsync(cancellationToken: cancellationToken);

            // TODO: get stream of inventories

            var response = new GetProductsResponse();
            response.Products.AddRange(products);

            return response;
        }
    }
}
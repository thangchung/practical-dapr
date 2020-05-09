using System;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Dtos;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure.Data;

namespace CoolStore.ProductCatalogApi.Application.UpdateProduct
{
    public class UpdateCreatedHandler : IRequestHandler<UpdateProductCommand, CatalogProductDto>
    {
        private readonly ProductCatalogDbContext _dbContext;

        public UpdateCreatedHandler(ProductCatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [TransactionScope]
        public async Task<CatalogProductDto> Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (product == null)
            {
                throw new Exception($"Couldn't find product # {request.Id}");
            }

            product.UpdateProduct(
                request.Name,
                request.Description,
                request.Price,
                request.ImageUrl,
                request.StoreId,
                request.Rop,
                request.Eoq);

            var cats = await _dbContext.Categories.ToListAsync(cancellationToken: cancellationToken);
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken: cancellationToken);

            if (category == null)
            {
                throw new NullReferenceException($"Couldn't find out any Category # {request.CategoryId}");
            }

            product.AssignCategory(category);

            var entityUpdated = _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var productUpdated = entityUpdated.Entity;

            return new CatalogProductDto
            {
                Id = productUpdated.Id,
                Name = productUpdated.Name,
                Description = productUpdated.Description,
                ImageUrl = productUpdated.ImageUrl,
                Price = productUpdated.Price
            };
        }
    }
}

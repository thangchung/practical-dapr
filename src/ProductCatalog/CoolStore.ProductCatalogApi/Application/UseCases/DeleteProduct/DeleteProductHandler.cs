using System;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure.Data;

namespace CoolStore.ProductCatalogApi.Application.UseCases.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly ProductCatalogDbContext _dbContext;

        public DeleteProductHandler(ProductCatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [TransactionScope]
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (product == null)
            {
                throw new Exception($"Couldn't find product #{request.Id}");
            }

            product.MarkAsDeleted();

            var effected = await _dbContext.SaveChangesAsync(cancellationToken);
            return effected > 0;
        }
    }
}

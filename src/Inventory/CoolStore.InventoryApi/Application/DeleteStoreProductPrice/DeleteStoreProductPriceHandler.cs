using System;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure.Data;

namespace CoolStore.InventoryApi.Application.DeleteStoreProductPrice
{
    public class DeleteStoreProductPriceHandler : IRequestHandler<DeleteStoreProductPriceCommand, bool>
    {
        private readonly InventoryDbContext _dbContext;

        public DeleteStoreProductPriceHandler(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [TransactionScope]
        public async Task<bool> Handle(
            DeleteStoreProductPriceCommand request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var storeProductPrice = await _dbContext.StoreProductPrices
                .FirstOrDefaultAsync(x => x.StoreId == request.StoreId && x.ProductId == request.ProductId);

            if (storeProductPrice == null)
            {
                throw new Exception($"Couldn't find any store-product-price # {request.StoreId} & {request.ProductId}");
            }

            _dbContext.StoreProductPrices.Remove(storeProductPrice);
            var effectedRows = await _dbContext.SaveChangesAsync();
            return effectedRows > 0;
        }
    }
}

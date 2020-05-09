using System;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Domain;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure.Data;

namespace CoolStore.InventoryApi.Application.AddStoreProductPrice
{
    public class AddStoreProductPriceHandler : IRequestHandler<AddStoreProductPriceCommand, bool>
    {
        private readonly InventoryDbContext _dbContext;

        public AddStoreProductPriceHandler(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [TransactionScope]
        public async Task<bool> Handle(
            AddStoreProductPriceCommand request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var store = await _dbContext.Stores
                .FirstOrDefaultAsync(x => x.Id == request.StoreId);

            if (store == null)
            {
                throw new Exception($"Couldn't find any store # {request.StoreId}");
            }

            var storeProductPrice = StoreProductPrice.Of(
                Guid.NewGuid(),
                store.Id,
                request.ProductId,
                request.Price,
                request.Rop,
                request.Eoq);

            await _dbContext.StoreProductPrices.AddAsync(storeProductPrice);
            var effectedRows = await _dbContext.SaveChangesAsync();
            return effectedRows > 0;
        }
    }
}

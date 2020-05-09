using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using CoolStore.Protobuf.Inventory.V1;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoolStore.InventoryApi.Application.GetStoresByIds
{
    public class GetStoresByIdsHandler : IRequestHandler<GetStoresByIdsQuery, IEnumerable<StoreDto>>
    {
        private readonly InventoryDbContext _dbContext;

        public GetStoresByIdsHandler(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<StoreDto>> Handle(
            GetStoresByIdsQuery request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var stores = await _dbContext.Stores
                .AsNoTracking()
                .Where(x => request.Ids.Contains(x.Id))
                .ToListAsync();

            if (stores == null)
            {
                throw new Exception("Could not get the records from the database.");
            }

            return stores.Select(x => new StoreDto
            {
                Id = x.Id.ToString(),
                Location = x.Location,
                Description = x.Description,
                Website = x.Website
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using CoolStore.Protobuf.Inventory.V1;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoolStore.InventoryApi.Application.UseCases.GetInventory
{
    public class GetInventoriesByIdsHandler : IRequestHandler<GetInventoriesByIdsQuery, IEnumerable<InventoryDto>>
    {
        private readonly InventoryDbContext _dbContext;

        public GetInventoriesByIdsHandler(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<InventoryDto>> Handle(GetInventoriesByIdsQuery request,
            CancellationToken cancellationToken)
        {
            var inventories = await _dbContext.Inventories
                .AsNoTracking()
                .Where(x => request.Ids.Contains(x.Id))
                .ToListAsync();

            if (inventories == null)
            {
                throw new Exception("Could not get the records from the database.");
            }

            return inventories.Select(x => new InventoryDto
            {
                Id = x.Id.ToString(), Location = x.Location, Description = x.Description, Website = x.Website
            });
        }
    }
}

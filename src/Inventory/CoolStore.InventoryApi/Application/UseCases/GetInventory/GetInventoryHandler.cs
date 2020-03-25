using System;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using CoolStore.InventoryApi.Persistence;
using CoolStore.Protobuf.Inventory.V1;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoolStore.InventoryApi.Application.UseCases.GetInventory
{
    public class GetInventoryHandler : IRequestHandler<GetInventoryQuery, InventoryDto>
    {
        private readonly InventoryDbContext _dbContext;

        public GetInventoryHandler(InventoryDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<InventoryDto> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
        {
            var inventory = await _dbContext.Inventories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (inventory == null)
            {
                throw new Exception("Could not get the record from the database.");
            }

            return new InventoryDto
            {
                Id = inventory.Id.ToString(),
                Location = inventory.Location,
                Description = inventory.Description,
                Website = inventory.Website
            };
        }
    }
}
using System.Linq;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using CoolStore.Protobuf.Inventory.V1;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoolStore.InventoryApi.Application.GetAvailabilityStores
{
    public class GetAvailabilityStoresHandler : RequestHandler<GetStoresQuery, IQueryable<StoreDto>>
    {
        private readonly InventoryDbContext _dbContext;

        public GetAvailabilityStoresHandler(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override IQueryable<StoreDto> Handle(
            GetStoresQuery request)
        {
            if (request is null)
            {
                throw new System.ArgumentNullException(nameof(request));
            }

            return _dbContext.Stores
                .AsNoTracking()
                .Select(x => new StoreDto
                {
                    Id = x.Id.ToString(),
                    Location = x.Location,
                    Description = x.Description,
                    Website = x.Website
                });
        }
    }
}

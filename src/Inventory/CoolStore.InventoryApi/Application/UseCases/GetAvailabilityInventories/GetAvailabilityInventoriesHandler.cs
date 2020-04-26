using System;
using System.Linq;
using CoolStore.InventoryApi.Dtos;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoolStore.InventoryApi.Application.UseCases.GetAvailabilityInventories
{
    public class GetAvailabilityInventoriesHandler : RequestHandler<GetInventoriesQuery, IQueryable<InventoryDto>>
    {
        private readonly InventoryDbContext _dbContext;

        public GetAvailabilityInventoriesHandler(InventoryDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override IQueryable<InventoryDto> Handle(GetInventoriesQuery request)
        {
            return _dbContext.Inventories
                .AsNoTracking()
                .Select(x => new InventoryDto
                {
                    Id = x.Id, Location = x.Location, Description = x.Description, Website = x.Website
                });
        }
    }
}

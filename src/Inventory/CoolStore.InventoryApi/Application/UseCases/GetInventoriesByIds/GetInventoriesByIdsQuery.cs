using System;
using System.Collections.Generic;
using CoolStore.InventoryApi.Dtos;
using MediatR;

namespace CoolStore.InventoryApi.Application.UseCases.GetInventory
{
    public class GetInventoriesByIdsQuery : IRequest<IEnumerable<InventoryDto>>
    {
        public IEnumerable<Guid> Ids { get; set; } = new List<Guid>();
    }
}

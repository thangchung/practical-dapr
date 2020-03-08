using System;
using CoolStore.Protobuf.Inventory.V1;
using MediatR;

namespace CoolStore.InventoryApi.UseCases.GetInventory
{
    public class GetInventoryQuery : IRequest<InventoryDto>
    {
        public Guid Id { get; set; }
    }
}
using System;
using System.Collections.Generic;
using CoolStore.Protobuf.Inventory.V1;
using MediatR;

namespace CoolStore.InventoryApi.Application.GetStoresByIds
{
    public class GetStoresByIdsQuery : IRequest<IEnumerable<StoreDto>>
    {
        public IEnumerable<Guid> Ids { get; set; } = new List<Guid>();
    }
}

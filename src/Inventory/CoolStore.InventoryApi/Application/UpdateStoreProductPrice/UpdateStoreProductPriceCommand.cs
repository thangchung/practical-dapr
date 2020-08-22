using System;
using MediatR;
using N8T.Infrastructure.Data;

namespace CoolStore.InventoryApi.Application.UpdateStoreProductPrice
{
    public class UpdateStoreProductPriceCommand : IRequest<bool>, ITxRequest
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime CreatedAt { get; }
        public Guid StoreId { get; set; }
        public int Rop { get; set; } = 5;
        public int Eoq { get; set; } = 10;
    }
}

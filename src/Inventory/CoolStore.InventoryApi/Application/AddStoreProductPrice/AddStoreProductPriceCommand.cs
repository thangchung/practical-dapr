using System;
using MediatR;

namespace CoolStore.InventoryApi.Application.AddStoreProductPrice
{
    public class AddStoreProductPriceCommand : IRequest<bool>
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

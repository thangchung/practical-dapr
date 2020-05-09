using System;
using MediatR;

namespace CoolStore.InventoryApi.Application.DeleteStoreProductPrice
{
    public class DeleteStoreProductPriceCommand : IRequest<bool>
    {
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }
    }
}

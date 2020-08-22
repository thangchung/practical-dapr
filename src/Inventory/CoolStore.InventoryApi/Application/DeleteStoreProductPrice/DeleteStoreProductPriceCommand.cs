using System;
using MediatR;
using N8T.Infrastructure.Data;

namespace CoolStore.InventoryApi.Application.DeleteStoreProductPrice
{
    public class DeleteStoreProductPriceCommand : IRequest<bool>, ITxRequest
    {
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }
    }
}

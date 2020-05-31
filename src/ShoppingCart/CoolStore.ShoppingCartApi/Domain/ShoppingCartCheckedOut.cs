using System;
using N8T.Domain;

namespace CoolStore.ShoppingCartApi.Domain
{
    public class ShoppingCartCheckedOut : DomainEventBase
    {
        public Guid CartId { get; set; }
    }
}

using System;
using N8T.Domain;

namespace CoolStore.ProductCatalogApi.Domain
{
    public class ProductDeleted: DomainEventBase
    {
        public Guid ProductId { get; set; }
    }
}

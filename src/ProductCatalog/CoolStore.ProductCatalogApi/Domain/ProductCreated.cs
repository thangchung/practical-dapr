using System;
using N8T.Domain;

namespace CoolStore.ProductCatalogApi.Domain
{
    public class ProductCreated : DomainEventBase
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public Guid StoreId { get; set; }
        public int Rop { get; set; }
        public int Eoq { get; set; }
    }
}

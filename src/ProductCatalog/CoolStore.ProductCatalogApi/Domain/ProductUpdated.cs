using System;
using N8T.Domain;

namespace CoolStore.ProductCatalogApi.Domain
{
    public class ProductUpdated: DomainEventBase
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string ImageUrl { get; set; } = "https://picsum.photos/1200/900?image=1";
        public string? Description { get; set; }
    }
}

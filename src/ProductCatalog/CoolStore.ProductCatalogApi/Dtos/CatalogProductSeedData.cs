using System;

namespace CoolStore.ProductCatalogApi.Dtos
{
    public class CatalogProductSeedData
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; } = "https://picsum.photos/1200/900?image=1";
        public Guid InventoryId { get; set; }
        public string InventoryLocation { get; set; } = string.Empty;
        public string InventoryWebsite { get; set; } = string.Empty;
        public string? InventoryDescription { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}

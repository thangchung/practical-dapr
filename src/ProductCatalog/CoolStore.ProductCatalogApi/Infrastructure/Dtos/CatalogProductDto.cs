using System;

namespace CoolStore.ProductCatalogApi.Dtos
{
    public class CatalogProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; } = "https://picsum.photos/1200/900?image=1";
        public Guid InventoryId { get; set; }
        public CategoryDto Category { get; set; } = new CategoryDto();
    }
}

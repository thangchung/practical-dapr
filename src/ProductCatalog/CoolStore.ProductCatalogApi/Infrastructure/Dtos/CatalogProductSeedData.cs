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
        public Guid StoreId { get; set; }
        public string StoreLocation { get; set; } = string.Empty;
        public string StoreWebsite { get; set; } = string.Empty;
        public string? StoreDescription { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}

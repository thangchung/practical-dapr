using System;

namespace CoolStore.ProductCatalogApi.Dtos
{
    public class InventoryDto
    {
        public Guid Id { get; set; }
        public string Location { get; set; } = "Vietnam";
        public string? Description { get; set; }
        public string Website { get; set; } = "https://coolstore-vn.com";
    }
}

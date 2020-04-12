using System.Collections.Generic;

namespace CoolStore.WebUI.Models
{
    public class ProductModel
    {
        public int TotalCount { get; set; }
        public List<ProductItemModel> Edges { get; set; }
    }

    public class ProductItemModel
    {
        public string Name { get; set; }
        public string InventoryLocation { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
    }
}

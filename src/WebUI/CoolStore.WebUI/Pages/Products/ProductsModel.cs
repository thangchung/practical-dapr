using System;
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
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public InventoryModel Inventory { get; set; } = new InventoryModel();
    }

    public class InventoryModel
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
    }
}

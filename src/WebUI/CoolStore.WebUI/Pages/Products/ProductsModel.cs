using System;
using System.Collections.Generic;

namespace CoolStore.WebUI.Models
{
    public class ProductModel
    {
        public int TotalCount { get; set; } = 0;
        public List<ProductItemModel> Items { get; set; } = new List<ProductItemModel>();
    }

    public class ProductItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public Guid InventoryId { get; set; }
        public string InventoryLocation { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

        //public InventoryModel Inventory { get; set; } = new InventoryModel();
    }

    public class InventoryModel
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
    }
}

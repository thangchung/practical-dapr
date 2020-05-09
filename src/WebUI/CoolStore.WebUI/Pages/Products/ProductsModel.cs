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
        public Guid StoreId { get; set; }
        public string StoreLocation { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

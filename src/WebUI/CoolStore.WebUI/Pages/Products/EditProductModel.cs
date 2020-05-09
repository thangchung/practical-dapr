using System;
using System.ComponentModel.DataAnnotations;

namespace CoolStore.WebUI.Models
{
    public class EditProductModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        [Required] public string CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        [Required] public string ImageUrl { get; set; } = "https://picsum.photos/1200/900?image=100";
        [Required] public string StoreId { get; set; }
        [Required] public double Price { get; set; } = 1000;
        [Required] public int Rop { get; set; } = 5;
        [Required] public int Eoq { get; set; } = 10;
    }

    public class ProductItemDetailModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public Guid StoreId { get; set; }
        public string StoreLocation { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Rop { get; set; }
        public int Eoq { get; set; }
    }
}

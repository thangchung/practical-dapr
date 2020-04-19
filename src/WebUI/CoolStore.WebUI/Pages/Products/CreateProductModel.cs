using System;
using System.ComponentModel.DataAnnotations;

namespace CoolStore.WebUI.Models
{
    public class CreateProductModel
    {
        [Required]
        [StringLength(10, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        [Required] public Guid CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required] public string ImageUrl { get; set; } = "https://picsum.photos/1200/900?image=100";

        [Required] public Guid InventoryId { get; set; }

        [Required] public double Price { get; set; } = 1000;
    }

    public class KeyValueModel
    {
        public Guid Key { get; set; }
        public string Value { get; set; }
    }
}

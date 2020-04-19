using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class CatalogProductDto
        : ICatalogProductDto
    {
        public CatalogProductDto(
            System.Guid id, 
            string name, 
            string imageUrl, 
            double price, 
            string description, 
            global::CoolStore.WebUI.Host.ICategoryDto category, 
            global::CoolStore.WebUI.Host.IInventoryDto inventory)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Price = price;
            Description = description;
            Category = category;
            Inventory = inventory;
        }

        public System.Guid Id { get; }

        public string Name { get; }

        public string ImageUrl { get; }

        public double Price { get; }

        public string Description { get; }

        public global::CoolStore.WebUI.Host.ICategoryDto Category { get; }

        public global::CoolStore.WebUI.Host.IInventoryDto Inventory { get; }
    }
}

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
            string id, 
            string name, 
            string imageUrl, 
            double price, 
            string categoryId, 
            string categoryName, 
            string inventoryId, 
            string inventoryLocation)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Price = price;
            CategoryId = categoryId;
            CategoryName = categoryName;
            InventoryId = inventoryId;
            InventoryLocation = inventoryLocation;
        }

        public string Id { get; }

        public string Name { get; }

        public string ImageUrl { get; }

        public double Price { get; }

        public string CategoryId { get; }

        public string CategoryName { get; }

        public string InventoryId { get; }

        public string InventoryLocation { get; }
    }
}

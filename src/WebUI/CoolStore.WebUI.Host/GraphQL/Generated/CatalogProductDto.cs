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
            string name, 
            string inventoryLocation)
        {
            Name = name;
            InventoryLocation = inventoryLocation;
        }

        public string Name { get; }

        public string InventoryLocation { get; }
    }
}

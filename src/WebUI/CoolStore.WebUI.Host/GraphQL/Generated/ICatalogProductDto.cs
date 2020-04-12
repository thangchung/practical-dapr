using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial interface ICatalogProductDto
    {
        string Id { get; }

        string Name { get; }

        string ImageUrl { get; }

        double Price { get; }

        string CategoryId { get; }

        string CategoryName { get; }

        string InventoryId { get; }

        string InventoryLocation { get; }
    }
}

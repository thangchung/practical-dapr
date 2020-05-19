using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial interface ICatalogProductDto
    {
        System.Guid Id { get; }

        string Name { get; }

        string ImageUrl { get; }

        double Price { get; }

        string Description { get; }

        global::CoolStore.WebUI.Host.ICategoryDto Category { get; }

        global::CoolStore.WebUI.Host.IStoreDto Store { get; }
    }
}

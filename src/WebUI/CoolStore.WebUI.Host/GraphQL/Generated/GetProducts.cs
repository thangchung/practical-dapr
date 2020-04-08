using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetProducts
        : IGetProducts
    {
        public GetProducts(
            global::CoolStore.WebUI.Host.IOffsetPagingOfCatalogProductDto products)
        {
            Products = products;
        }

        public global::CoolStore.WebUI.Host.IOffsetPagingOfCatalogProductDto Products { get; }
    }
}

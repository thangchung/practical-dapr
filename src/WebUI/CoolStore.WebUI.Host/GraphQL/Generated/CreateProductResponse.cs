using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class CreateProductResponse
        : ICreateProductResponse
    {
        public CreateProductResponse(
            global::CoolStore.WebUI.Host.ICatalogProductDto1 product)
        {
            Product = product;
        }

        public global::CoolStore.WebUI.Host.ICatalogProductDto1 Product { get; }
    }
}

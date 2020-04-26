using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class CreateProductMutation
        : ICreateProductMutation
    {
        public CreateProductMutation(
            global::CoolStore.WebUI.Host.ICatalogProductDto1 createProduct)
        {
            CreateProduct = createProduct;
        }

        public global::CoolStore.WebUI.Host.ICatalogProductDto1 CreateProduct { get; }
    }
}

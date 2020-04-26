using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class UpdateProductMutation
        : IUpdateProductMutation
    {
        public UpdateProductMutation(
            global::CoolStore.WebUI.Host.ICatalogProductDto2 updateProduct)
        {
            UpdateProduct = updateProduct;
        }

        public global::CoolStore.WebUI.Host.ICatalogProductDto2 UpdateProduct { get; }
    }
}

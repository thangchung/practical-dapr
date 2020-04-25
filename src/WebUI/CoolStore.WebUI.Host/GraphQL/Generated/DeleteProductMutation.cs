using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class DeleteProductMutation
        : IDeleteProductMutation
    {
        public DeleteProductMutation(
            bool deleteProduct)
        {
            DeleteProduct = deleteProduct;
        }

        public bool DeleteProduct { get; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class CatalogProductDto1
        : ICatalogProductDto1
    {
        public CatalogProductDto1(
            System.Guid id, 
            string name)
        {
            Id = id;
            Name = name;
        }

        public System.Guid Id { get; }

        public string Name { get; }
    }
}

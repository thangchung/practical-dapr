using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class CatalogProductDtoFilter
    {
        public Optional<global::System.Collections.Generic.List<global::CoolStore.WebUI.Host.CatalogProductDtoFilter>> AND { get; set; }

        public Optional<string> Name { get; set; }

        public Optional<string> NameContains { get; set; }

        public Optional<global::System.Collections.Generic.List<global::CoolStore.WebUI.Host.CatalogProductDtoFilter>> OR { get; set; }

        public Optional<double?> Price { get; set; }

        public Optional<double?> PriceGte { get; set; }

        public Optional<double?> PriceLte { get; set; }
    }
}

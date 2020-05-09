using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class UpdateProductInput
    {
        public Optional<System.Guid> CategoryId { get; set; }

        public Optional<string> Description { get; set; }

        public Optional<int> Eoq { get; set; }

        public Optional<System.Guid> Id { get; set; }

        public Optional<string> ImageUrl { get; set; }

        public Optional<string> Name { get; set; }

        public Optional<double> Price { get; set; }

        public Optional<int> Rop { get; set; }

        public Optional<System.Guid> StoreId { get; set; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class InventoryDto1
        : IInventoryDto1
    {
        public InventoryDto1(
            System.Guid id, 
            string location)
        {
            Id = id;
            Location = location;
        }

        public System.Guid Id { get; }

        public string Location { get; }
    }
}

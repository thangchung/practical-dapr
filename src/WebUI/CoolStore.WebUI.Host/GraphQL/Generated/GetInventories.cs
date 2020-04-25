using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetInventories
        : IGetInventories
    {
        public GetInventories(
            global::System.Collections.Generic.IReadOnlyList<global::CoolStore.WebUI.Host.IInventoryDto1> inventories)
        {
            Inventories = inventories;
        }

        public global::System.Collections.Generic.IReadOnlyList<global::CoolStore.WebUI.Host.IInventoryDto1> Inventories { get; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetCategories
        : IGetCategories
    {
        public GetCategories(
            global::System.Collections.Generic.IReadOnlyList<global::CoolStore.WebUI.Host.ICategoryDto1> categories)
        {
            Categories = categories;
        }

        public global::System.Collections.Generic.IReadOnlyList<global::CoolStore.WebUI.Host.ICategoryDto1> Categories { get; }
    }
}

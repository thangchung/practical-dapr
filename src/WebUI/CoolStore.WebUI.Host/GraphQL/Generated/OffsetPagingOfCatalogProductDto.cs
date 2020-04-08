using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class OffsetPagingOfCatalogProductDto
        : IOffsetPagingOfCatalogProductDto
    {
        public OffsetPagingOfCatalogProductDto(
            global::System.Collections.Generic.IReadOnlyList<global::CoolStore.WebUI.Host.ICatalogProductDto> edges, 
            int totalCount)
        {
            Edges = edges;
            TotalCount = totalCount;
        }

        public global::System.Collections.Generic.IReadOnlyList<global::CoolStore.WebUI.Host.ICatalogProductDto> Edges { get; }

        public int TotalCount { get; }
    }
}

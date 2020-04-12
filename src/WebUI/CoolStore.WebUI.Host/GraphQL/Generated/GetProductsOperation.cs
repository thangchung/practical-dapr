using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetProductsOperation
        : IOperation<IGetProducts>
    {
        public string Name => "getProducts";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Query;

        public Type ResultType => typeof(IGetProducts);

        public Optional<int?> Page { get; set; }

        public Optional<int?> PageSize { get; set; }

        public Optional<global::CoolStore.WebUI.Host.CatalogProductDtoFilter> Where { get; set; }

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            var variables = new List<VariableValue>();

            if (Page.HasValue)
            {
                variables.Add(new VariableValue("page", "Int", Page.Value));
            }

            if (PageSize.HasValue)
            {
                variables.Add(new VariableValue("pageSize", "Int", PageSize.Value));
            }

            if (Where.HasValue)
            {
                variables.Add(new VariableValue("where", "CatalogProductDtoFilter", Where.Value));
            }

            return variables;
        }
    }
}

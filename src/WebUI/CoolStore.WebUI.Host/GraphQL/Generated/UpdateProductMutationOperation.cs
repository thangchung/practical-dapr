using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class UpdateProductMutationOperation
        : IOperation<IUpdateProductMutation>
    {
        public string Name => "updateProductMutation";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Mutation;

        public Type ResultType => typeof(IUpdateProductMutation);

        public Optional<global::CoolStore.WebUI.Host.UpdateProductInput> UpdateProductInput { get; set; }

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            var variables = new List<VariableValue>();

            if (UpdateProductInput.HasValue)
            {
                variables.Add(new VariableValue("updateProductInput", "UpdateProductInput", UpdateProductInput.Value));
            }

            return variables;
        }
    }
}

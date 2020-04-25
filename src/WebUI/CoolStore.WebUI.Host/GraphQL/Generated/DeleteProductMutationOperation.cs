using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class DeleteProductMutationOperation
        : IOperation<IDeleteProductMutation>
    {
        public string Name => "deleteProductMutation";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Mutation;

        public Type ResultType => typeof(IDeleteProductMutation);

        public Optional<global::CoolStore.WebUI.Host.DeleteProductInput> DeleteProductInput { get; set; }

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            var variables = new List<VariableValue>();

            if (DeleteProductInput.HasValue)
            {
                variables.Add(new VariableValue("deleteProductInput", "DeleteProductInput", DeleteProductInput.Value));
            }

            return variables;
        }
    }
}

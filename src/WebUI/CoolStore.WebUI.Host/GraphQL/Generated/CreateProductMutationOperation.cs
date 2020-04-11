using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class CreateProductMutationOperation
        : IOperation<ICreateProductMutation>
    {
        public string Name => "createProductMutation";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Mutation;

        public Type ResultType => typeof(ICreateProductMutation);

        public Optional<global::CoolStore.WebUI.Host.CreateProductInput> CreateProductInput { get; set; }

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            var variables = new List<VariableValue>();

            if (CreateProductInput.HasValue)
            {
                variables.Add(new VariableValue("createProductInput", "CreateProductInput", CreateProductInput.Value));
            }

            return variables;
        }
    }
}

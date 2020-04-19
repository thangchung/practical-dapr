using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GraphQLClient
        : IGraphQLClient
    {
        private const string _clientName = "GraphQLClient";

        private readonly global::StrawberryShake.IOperationExecutor _executor;

        public GraphQLClient(global::StrawberryShake.IOperationExecutorPool executorPool)
        {
            _executor = executorPool.CreateExecutor(_clientName);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.IGetProducts>> GetProductsAsync(
            global::StrawberryShake.Optional<int?> page = default,
            global::StrawberryShake.Optional<int?> pageSize = default,
            global::StrawberryShake.Optional<global::CoolStore.WebUI.Host.CatalogProductDtoFilter> where = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {

            return _executor.ExecuteAsync(
                new GetProductsOperation
                {
                    Page = page, 
                    PageSize = pageSize, 
                    Where = where
                },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.IGetProducts>> GetProductsAsync(
            GetProductsOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.ICreateProductMutation>> CreateProductMutationAsync(
            global::StrawberryShake.Optional<global::CoolStore.WebUI.Host.CreateProductInput> createProductInput = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (createProductInput.HasValue && createProductInput.Value is null)
            {
                throw new ArgumentNullException(nameof(createProductInput));
            }

            return _executor.ExecuteAsync(
                new CreateProductMutationOperation { CreateProductInput = createProductInput },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.ICreateProductMutation>> CreateProductMutationAsync(
            CreateProductMutationOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }
    }
}

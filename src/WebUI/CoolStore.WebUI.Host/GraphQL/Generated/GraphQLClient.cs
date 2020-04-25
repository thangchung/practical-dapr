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

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.IGetCategories>> GetCategoriesAsync(
            global::System.Threading.CancellationToken cancellationToken = default)
        {

            return _executor.ExecuteAsync(
                new GetCategoriesOperation(),
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.IGetCategories>> GetCategoriesAsync(
            GetCategoriesOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.IGetInventories>> GetInventoriesAsync(
            global::System.Threading.CancellationToken cancellationToken = default)
        {

            return _executor.ExecuteAsync(
                new GetInventoriesOperation(),
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.IGetInventories>> GetInventoriesAsync(
            GetInventoriesOperation operation,
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

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.IUpdateProductMutation>> UpdateProductMutationAsync(
            global::StrawberryShake.Optional<global::CoolStore.WebUI.Host.UpdateProductInput> updateProductInput = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (updateProductInput.HasValue && updateProductInput.Value is null)
            {
                throw new ArgumentNullException(nameof(updateProductInput));
            }

            return _executor.ExecuteAsync(
                new UpdateProductMutationOperation { UpdateProductInput = updateProductInput },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.IUpdateProductMutation>> UpdateProductMutationAsync(
            UpdateProductMutationOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.IDeleteProductMutation>> DeleteProductMutationAsync(
            global::StrawberryShake.Optional<global::CoolStore.WebUI.Host.DeleteProductInput> deleteProductInput = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (deleteProductInput.HasValue && deleteProductInput.Value is null)
            {
                throw new ArgumentNullException(nameof(deleteProductInput));
            }

            return _executor.ExecuteAsync(
                new DeleteProductMutationOperation { DeleteProductInput = deleteProductInput },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::CoolStore.WebUI.Host.IDeleteProductMutation>> DeleteProductMutationAsync(
            DeleteProductMutationOperation operation,
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

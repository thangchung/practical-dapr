using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial interface IGraphQLClient
    {
        Task<IOperationResult<global::CoolStore.WebUI.Host.IGetProducts>> GetProductsAsync(
            Optional<int?> page = default,
            Optional<int?> pageSize = default,
            Optional<global::CoolStore.WebUI.Host.CatalogProductDtoFilter> where = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.IGetProducts>> GetProductsAsync(
            GetProductsOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.IGetCategories>> GetCategoriesAsync(
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.IGetCategories>> GetCategoriesAsync(
            GetCategoriesOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.IGetInventories>> GetInventoriesAsync(
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.IGetInventories>> GetInventoriesAsync(
            GetInventoriesOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.ICreateProductMutation>> CreateProductMutationAsync(
            Optional<global::CoolStore.WebUI.Host.CreateProductInput> createProductInput = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.ICreateProductMutation>> CreateProductMutationAsync(
            CreateProductMutationOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.IUpdateProductMutation>> UpdateProductMutationAsync(
            Optional<global::CoolStore.WebUI.Host.UpdateProductInput> updateProductInput = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.IUpdateProductMutation>> UpdateProductMutationAsync(
            UpdateProductMutationOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.IDeleteProductMutation>> DeleteProductMutationAsync(
            Optional<global::CoolStore.WebUI.Host.DeleteProductInput> deleteProductInput = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.IDeleteProductMutation>> DeleteProductMutationAsync(
            DeleteProductMutationOperation operation,
            CancellationToken cancellationToken = default);
    }
}

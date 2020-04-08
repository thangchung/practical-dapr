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
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::CoolStore.WebUI.Host.IGetProducts>> GetProductsAsync(
            GetProductsOperation operation,
            CancellationToken cancellationToken = default);
    }
}

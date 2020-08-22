using System;
using MediatR;
using N8T.Infrastructure.Data;

namespace CoolStore.ProductCatalogApi.Application.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>, ITxRequest
    {
        public Guid Id { get; set; }
    }
}

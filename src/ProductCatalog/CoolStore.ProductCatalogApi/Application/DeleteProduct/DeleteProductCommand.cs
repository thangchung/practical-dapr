using System;
using MediatR;

namespace CoolStore.ProductCatalogApi.Application.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}

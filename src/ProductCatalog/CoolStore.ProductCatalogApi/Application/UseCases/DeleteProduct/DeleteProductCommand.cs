using System;
using MediatR;

namespace CoolStore.ProductCatalogApi.Application.UseCases.DeleteProduct
{
    public class DeleteProductCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}

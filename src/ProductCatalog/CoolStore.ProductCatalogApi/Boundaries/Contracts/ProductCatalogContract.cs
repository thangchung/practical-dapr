using MediatR;
using N8T.Domain;
using System;
using static N8T.Infrastructure.Helpers.DateTimeHelper;

// ReSharper disable CheckNamespace
namespace CoolStore.Protobuf.ProductCatalog.V1
{
    public partial class GetProductsRequest : IRequest<GetProductsResponse> { }
    public partial class GetProductByIdRequest : IRequest<GetProductByIdResponse> { }
    public partial class CreateProductRequest : IRequest<CreateProductResponse> { }
    public partial class UpdateProductRequest : IRequest<UpdateProductResponse> { }
    public partial class DeleteProductRequest : IRequest<DeleteProductResponse> { }

    public partial class ProductCreated : IDomainEvent
    {
        public DateTime CreatedAt => NewDateTime();
    }

    public partial class ProductUpdated : IDomainEvent
    {
        public DateTime CreatedAt => NewDateTime();
    }

    public partial class ProductDeleted : IDomainEvent
    {
        public DateTime CreatedAt => NewDateTime();
    }
}
using MediatR;
using N8T.Domain;
using N8T.Infrastructure.Data;
using System;
using static N8T.Infrastructure.Helpers.DateTimeHelper;

// ReSharper disable CheckNamespace
namespace CoolStore.Protobuf.ProductCatalog.V1
{
    public partial class GetProductByIdRequest : IRequest<GetProductByIdResponse> { }
    [TransactionScope]
    public partial class CreateProductRequest : IRequest<CreateProductResponse> { }
    [TransactionScope]
    public partial class UpdateProductRequest : IRequest<UpdateProductResponse> { }
    [TransactionScope]
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
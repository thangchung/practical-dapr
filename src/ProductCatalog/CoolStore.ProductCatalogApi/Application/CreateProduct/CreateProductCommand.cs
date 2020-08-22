using System;
using CoolStore.ProductCatalogApi.Dtos;
using MediatR;
using N8T.Infrastructure.Data;

namespace CoolStore.ProductCatalogApi.Application.CreateProduct
{
    public class CreateProductCommand : IRequest<CatalogProductDto>, ITxRequest
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Price { get; set; } = default!;
        public string ImageUrl { get; set; } = "https://picsum.photos/1200/900?image=1";
        public Guid StoreId { get; set; } = default!;
        public Guid CategoryId { get; set; } = default!;
        public int Rop { get; set; } = 5;
        public int Eoq { get; set; } = 10;
    }
}

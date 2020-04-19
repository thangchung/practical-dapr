using System;
using CoolStore.ProductCatalogApi.Dtos;
using MediatR;

namespace CoolStore.ProductCatalogApi.Application.UseCase.CreateProduct
{
    public class CreateProductRequest : IRequest<CatalogProductDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; } = "https://picsum.photos/1200/900?image=1";
        public Guid InventoryId { get; set; }
        public Guid CategoryId { get; set; }
    }
}

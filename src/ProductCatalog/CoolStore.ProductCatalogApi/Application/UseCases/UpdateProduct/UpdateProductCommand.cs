using System;
using CoolStore.ProductCatalogApi.Dtos;
using MediatR;

namespace CoolStore.ProductCatalogApi.Application.UseCases.UpdateProduct
{
    public class UpdateProductCommand : IRequest<CatalogProductDto>
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Price { get; set; } = default!;
        public string ImageUrl { get; set; } = "https://picsum.photos/1200/900?image=1";
        public Guid InventoryId { get; set; } = default!;
        public Guid CategoryId { get; set; } = default!;
    }
}

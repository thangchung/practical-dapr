using CoolStore.Protobuf.ProductCatalog.V1;
using N8T.Domain;
using N8T.Infrastructure;
using System;
using static N8T.Infrastructure.Helpers.DateTimeHelper;

namespace CoolStore.ProductCatalogApi.Domain
{
    public class Product : EntityBase, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public string ImageUrl { get; private set; }
        public Guid InventoryId { get; private set; }
        public bool IsDeleted { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        private Product()
        {
        }

        public static Product Of(Guid productId, CreateProductRequest request)
        {
            var newProduct = new Product
            {
                Id = productId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                InventoryId = request.InventoryId.ConvertTo<Guid>(),
                CategoryId = request.CategoryId.ConvertTo<Guid>(),
                Created = NewDateTime(),
                IsDeleted = false
            };

            newProduct.AddDomainEvent(new ProductCreated
            {
                ProductId = productId.ToString(),
                Name = request.Name,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                Description = request.Description
            });

            return newProduct;
        }

        public Product UpdateProduct(UpdateProductRequest request)
        {
            Name = request.Name;
            Description = request.Description;
            Price = request.Price;
            ImageUrl = request.ImageUrl;

            if (!string.IsNullOrEmpty(request.InventoryId))
            {
                InventoryId = request.InventoryId.ConvertTo<Guid>();
            }

            Updated = NewDateTime();

            AddDomainEvent(new ProductUpdated
            {
                ProductId = Id.ToString(),
                Name = Name,
                Price = Price,
                ImageUrl = ImageUrl,
                Description = Description
            });

            return this;
        }

        public Product MarkAsDeleted()
        {
            IsDeleted = true;

            AddDomainEvent(new ProductDeleted
            {
                ProductId = Id.ToString()
            });

            return this;
        }
    }
}
using N8T.Domain;
using System;
using static N8T.Infrastructure.Helpers.DateTimeHelper;

namespace CoolStore.ProductCatalogApi.Domain
{
    public class Product : EntityBase, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }
        public double Price { get; private set; }
        public string ImageUrl { get; private set; } = "https://picsum.photos/1200/900?image=1";
        public Guid InventoryId { get; private set; }
        public bool IsDeleted { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        private Product()
        {
        }

        public static Product Of(Guid productId, string name, string? description, double price, string imageUrl,
            Guid inventoryId, Guid categoryId)
        {
            var newProduct = new Product
            {
                Id = productId,
                Name = name,
                Description = description,
                Price = price,
                ImageUrl = imageUrl,
                InventoryId = inventoryId,
                CategoryId = categoryId,
                Created = NewDateTime(),
                IsDeleted = false
            };

            newProduct.AddDomainEvent(new ProductCreated
            {
                ProductId = productId,
                Name = name,
                Price = price,
                ImageUrl = imageUrl,
                Description = description
            });

            return newProduct;
        }

        public Product AssignCategory(Category cat)
        {
            CategoryId = cat.Id;
            Category = cat;
            return this;
        }

        public Product UpdateProduct(string name, string? description, double price, string imageUrl, Guid inventoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;

            if (inventoryId == Guid.Empty)
            {
                throw new InventoryNullException();
            }
            else
            {
                InventoryId = inventoryId;
            }

            Updated = NewDateTime();

            AddDomainEvent(new ProductUpdated
            {
                ProductId = Id,
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
                ProductId = Id
            });

            return this;
        }
    }
}

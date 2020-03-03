using System;
using N8T.Domain;

namespace CoolStore.ProductCatalogApi.Domain
{
    public class Category : EntityBase, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private Category()
        {
        }

        public static Category Of(Guid id, string name)
        {
            return new Category {Id = id, Name = name};
        }
    }
}
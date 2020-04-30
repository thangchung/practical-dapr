using System;
using N8T.Domain;

namespace CoolStore.InventoryApi.Domain
{
    public class Store : EntityBase, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Location { get; private set; } = default!;
        public string? Description { get; private set; }
        public string Website { get; private set; } = default!;

        private Store()
        {
        }

        public static Store Of(Guid id, string location, string? description, string website)
        {
            return new Store
            {
                Id = id,
                Location = location,
                Description = description,
                Website = website
            };
        }
    }
}

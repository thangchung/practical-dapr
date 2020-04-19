using System;
using N8T.Domain;

namespace CoolStore.InventoryApi.Domain
{
    public class Inventory : EntityBase, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Location { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public string Website { get; private set; } = string.Empty;

        private Inventory()
        {
        }

        public static Inventory Of(Guid id, string location, string? description, string website)
        {
            return new Inventory
            {
                Id = id,
                Location = location,
                Description = description,
                Website = website
            };
        }
    }
}

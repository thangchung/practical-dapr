using System;
using N8T.Domain;
using static N8T.Infrastructure.Helpers.DateTimeHelper;

namespace CoolStore.InventoryApi.Domain
{
    public class StoreProductPrice : EntityBase
    {
        public Guid Id { get; private set; }
        public Guid StoreId { get; private set; }
        public Store Store { get; private set; }
        public Guid ProductId { get; private set; }
        public double Price { get; private set; }
        public int Rop { get; set; } = default!; // Re-order Point
        public int Eoq { get; set; } = default!; // Economic Order Quantity

        private StoreProductPrice()
        {
        }

        public static StoreProductPrice Of(
            Guid id,
            Guid storeId,
            Guid productId,
            double price,
            int rop,
            int eoq)
        {
            return new StoreProductPrice
            {
                Id = id,
                StoreId = storeId,
                ProductId = productId,
                Price = price,
                Rop = rop,
                Eoq = eoq,
                Created = NewDateTime()
            };
        }

        public static StoreProductPrice Of(
            Guid id,
            Store store,
            Guid productId,
            double price,
            int rop,
            int eoq)
        {
            return new StoreProductPrice
            {
                Id = id,
                StoreId = store.Id,
                Store = store,
                ProductId = productId,
                Price = price,
                Rop = rop,
                Eoq = eoq,
                Created = NewDateTime()
            };
        }

        public StoreProductPrice UpdateStoreProductPrice(double price, int rop, int eoq)
        {
            Price = price;
            Rop = rop;
            Eoq = eoq;
            return this;
        }
    }
}

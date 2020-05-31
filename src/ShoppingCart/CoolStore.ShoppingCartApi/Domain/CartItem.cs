using System;
using N8T.Domain;

namespace CoolStore.ShoppingCartApi.Domain
{
    public class CartItem : EntityBase
    {
        public Guid Id { get; set; } = default!;

        public int Quantity { get; private set; } = default!;

        public Guid ProductId { get; private set; } = default!;

        public double Price { get; private set; } = default!; // duplicated to avoid much query

        private CartItem()
        {
        }

        public CartItem AccumulateQuantity(int quantity)
        {
            Quantity += quantity;
            return this;
        }

        public static CartItem Of(
            int quantity,
            Guid productId)
        {
            return new CartItem
            {
                Id = Guid.NewGuid(),
                Quantity = quantity,
                ProductId = productId
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using N8T.Domain;

namespace CoolStore.ShoppingCartApi.Domain
{
    public class Cart : EntityBase, IAggregateRoot
    {
        public Guid Id { get; private set; } = default!;

        public string UserId { get; private set; } = default!;

        public double CartItemTotal { get; private set; } = default!;

        public double CartTotal { get; private set; } = default!;

        public ICollection<CartItem> CartItems { get; private set; }

        public bool IsCheckout { get; private set; } = default!;

        private Cart()
        {
        }

        public CartItem FindCartItem(Guid productId)
        {
            var cartItem = CartItems.FirstOrDefault(x => x.ProductId == productId);
            return cartItem!;
        }

        public Cart InsertItemToCart(Guid productId, int quantity = 1)
        {
            CartItems.Add(CartItem.Of(quantity, productId));
            return this;
        }

        public Cart RemoveCartItem(Guid itemId)
        {
            CartItems = CartItems.Where(y => y.Id != itemId).ToList();
            return this;
        }

        public Cart AccumulateCartItemQuantity(Guid cartItemId, int quantity)
        {
            var cartItem = CartItems.FirstOrDefault(x => x.Id == cartItemId);

            if (cartItem == null)
                throw new CoreException($"Couldn't find cart item #{cartItemId}");

            cartItem.AccumulateQuantity(quantity);
            return this;
        }

        public Cart CalculateCartAsync(TaxType taxType)
        {
            if (CartItems != null && CartItems?.Count() > 0)
            {
                CartItemTotal = 0.0D;
                foreach (var cartItem in CartItems)
                {
                    CartItemTotal += cartItem.Price * cartItem.Quantity;
                }
            }

            switch (taxType)
            {
                case TaxType.NoTax:
                    CartTotal = CartItemTotal;
                    break;
                case TaxType.TenPercentage:
                    var cartTotal = CartItemTotal;
                    CartTotal = cartTotal * 10 / 100 + cartTotal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(taxType), taxType, null);
            }

            return this;
        }

        public Cart Checkout()
        {
            IsCheckout = true;
            AddDomainEvent(new ShoppingCartCheckedOut { CartId = Id });
            return this;
        }
    }
}

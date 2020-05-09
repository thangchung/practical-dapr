using System;
using N8T.Domain;
using static N8T.Infrastructure.Helpers.DateTimeHelper;

namespace CoolStore.ProductCatalogApi.Domain
{
    public class Rating : EntityBase, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid UserId { get; private set; }
        public int Cost { get; private set; } // 1->5

        private Rating()
        {
        }

        public static Rating Of(
            Guid id,
            Guid productId,
            Guid userId,
            int cost)
        {
            return new Rating
            {
                Id = id,
                ProductId = productId,
                UserId = userId,
                Cost = cost,
                Created = NewDateTime()
            };
        }

        public Rating UpdateRating(
            Guid userId,
            int cost)
        {
            UserId = userId;
            Cost = cost;
            return this;
        }
    }
}

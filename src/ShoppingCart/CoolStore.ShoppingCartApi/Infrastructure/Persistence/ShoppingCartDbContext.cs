using CoolStore.ShoppingCartApi.Domain;
using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure.Data;

namespace CoolStore.ShoppingCartApi.Infrastructure.Persistence
{
    public class ShoppingCartDbContextDesignFactory : DbContextDesignFactoryBase<ShoppingCartDbContext>
    {
    }

    public class ShoppingCartDbContext : AppDbContextBase
    {
        public ShoppingCartDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<CartItem> CartItems { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().ToTable("Cart", "cart");
            modelBuilder.Entity<CartItem>().ToTable("CartItem", "cart");

            modelBuilder.Entity<Cart>().HasKey(x => x.Id);
            modelBuilder.Entity<CartItem>().HasKey(x => x.Id);

            modelBuilder.Entity<Cart>().Ignore(x => x.DomainEvents);
            modelBuilder.Entity<CartItem>().Ignore(x => x.DomainEvents);

            modelBuilder.Entity<Cart>()
                .HasMany(x => x.CartItems);
        }
    }
}

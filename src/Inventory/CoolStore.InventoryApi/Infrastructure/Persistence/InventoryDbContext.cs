using System;
using System.Collections.Generic;
using CoolStore.InventoryApi.Domain;
using CoolStore.Protobuf.Inventory.V1;
using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;

namespace CoolStore.InventoryApi.Infrastructure.Persistence
{
    public class InventoryDbContextDesignFactory : DbContextDesignFactoryBase<InventoryDbContext>
    {
    }

    public class InventoryDbContext : AppDbContextBase
    {
        public InventoryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreProductPrice> StoreProductPrices { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>().ToTable("Store", "inv");
            modelBuilder.Entity<Store>().HasKey(x => x.Id);
            modelBuilder.Entity<Store>().Ignore(x => x.DomainEvents);

            modelBuilder.Entity<StoreProductPrice>().ToTable("StoreProductPrice", "inv");
            modelBuilder.Entity<StoreProductPrice>().HasKey(x => x.Id);
            modelBuilder.Entity<StoreProductPrice>().Ignore(x => x.DomainEvents);

            modelBuilder.Entity<Store>()
                .HasMany(x => x.StoreProductPrices)
                .WithOne(x => x.Store)
                .HasForeignKey(x => x.StoreId)
                .IsRequired();

            // seed data
            var models = "Infrastructure/Persistence/SeedData/inventories.json".ReadData<List<StoreDto>>(
                AppContext.BaseDirectory);
            //Console.WriteLine(models.SerializeObject());
            foreach (var inv in models)
            {
                modelBuilder.Entity<Store>().HasData(
                    Store.Of(
                        inv.Id.ConvertTo<Guid>(),
                        inv.Location,
                        inv.Description,
                        inv.Website
                    )
                );
            }

            var storeProducts = "Infrastructure/Persistence/SeedData/store-product-price.json"
                .ReadData<List<StoreProductPriceDto>>(
                    AppContext.BaseDirectory);
            //Console.WriteLine(storeProducts.SerializeObject());
            foreach (var sp in storeProducts)
            {
                modelBuilder.Entity<StoreProductPrice>().HasData(
                    StoreProductPrice.Of(
                        sp.Id.ConvertTo<Guid>(),
                        sp.StoreId.ConvertTo<Guid>(),
                        sp.ProductId.ConvertTo<Guid>(),
                        sp.Price,
                        sp.Rop,
                        sp.Eoq
                    )
                );
            }
        }
    }
}

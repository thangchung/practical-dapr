using System;
using System.Collections.Generic;
using CoolStore.InventoryApi.Domain;
using CoolStore.Protobuf.Inventory.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.Helpers;

namespace CoolStore.InventoryApi.Infrastructure.Persistence
{
    public class InventoryDbContext
        : AppDbContextBase
    {
        public InventoryDbContext(
            DbContextOptions options) : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>().ToTable("Store", "inv");
            modelBuilder.Entity<Store>().HasKey(x => x.Id);
            modelBuilder.Entity<Store>().Ignore(x => x.DomainEvents);

            // seed data
            var models = "Infrastructure/Persistence/SeedData/inventories.json".ReadData<List<StoreDto>>(AppContext.BaseDirectory);
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
        }
    }

    public class InventoryDbContextDesignFactory
        : IDesignTimeDbContextFactory<InventoryDbContext>
    {
        public InventoryDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)
                ?.GetConnectionString("MainDb");
            var optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseSqlServer(
                    connString ?? throw new InvalidOperationException(),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    }
                );

            Console.WriteLine(connString);
            return new InventoryDbContext(optionsBuilder.Options);
        }
    }
}

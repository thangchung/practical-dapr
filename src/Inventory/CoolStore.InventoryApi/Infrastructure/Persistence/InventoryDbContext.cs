using System;
using System.Collections.Generic;
using CoolStore.InventoryApi.Domain;
using CoolStore.InventoryApi.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.Helpers;

namespace CoolStore.InventoryApi.Infrastructure.Persistence
{
    public class InventoryDbContext : AppDbContextBase
    {
        public InventoryDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>().ToTable("Inventory", "inv");
            modelBuilder.Entity<Inventory>().HasKey(x => x.Id);
            modelBuilder.Entity<Inventory>().Ignore(x => x.DomainEvents);

            // seed data
            var models = "Infrastructure/Persistence/SeedData/inventories.json".ReadData<List<InventoryDto>>(AppContext.BaseDirectory);
            //Console.WriteLine(models.SerializeObject());
            foreach (var inv in models)
            {
                modelBuilder.Entity<Inventory>().HasData(
                    Inventory.Of(
                        inv.Id.ConvertTo<Guid>(),
                        inv.Location,
                        inv.Description,
                        inv.Website
                    )
                );
            }
        }
    }

    public class InventoryDbContextDesignFactory : IDesignTimeDbContextFactory<InventoryDbContext>
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

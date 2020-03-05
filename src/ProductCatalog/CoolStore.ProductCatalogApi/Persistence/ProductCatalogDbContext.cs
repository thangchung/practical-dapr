using System;
using System.Collections.Generic;
using CoolStore.ProductCatalogApi.Domain;
using CoolStore.Protobuf.ProductCatalog.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.Helpers;

namespace CoolStore.ProductCatalogApi.Persistence
{
    public class ProductCatalogDbContext : AppDbContextBase
    {
        public ProductCatalogDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Rating>().ToTable("Rating");

            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<Rating>().HasKey(x => x.Id);

            modelBuilder.Entity<Product>().Ignore(x => x.DomainEvents);
            modelBuilder.Entity<Category>().Ignore(x => x.DomainEvents);
            modelBuilder.Entity<Rating>().Ignore(x => x.DomainEvents);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();

            // seed data
            var categoryModels = "categories.json".ReadData<List<CategoryDto>>(AppContext.BaseDirectory);
            //Console.WriteLine(categoryModels.SerializeObject());
            foreach (var cat in categoryModels)
            {
                modelBuilder.Entity<Category>().HasData(
                    Category.Of(
                        cat.Id.ConvertTo<Guid>(),
                        cat.Name
                    )
                );
            }

            var productModels = "products.json".ReadData<List<CatalogProductDto>>(AppContext.BaseDirectory);
            //Console.WriteLine(productModels.SerializeObject());
            foreach (var prod in productModels)
            {
                modelBuilder.Entity<Product>().HasData(
                    Product.Of(
                        prod.Id.ConvertTo<Guid>(),
                        new CreateProductRequest
                        {
                            Name = prod.Name,
                            Description = prod.Description,
                            ImageUrl = prod.ImageUrl,
                            Price = prod.Price,
                            InventoryId = prod.InventoryId,
                            CategoryId = prod.CategoryId
                        }
                    )
                );
            }
        }
    }

    public class ProductCatalogDbContextDesignFactory : IDesignTimeDbContextFactory<ProductCatalogDbContext>
    {
        public ProductCatalogDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)
                ?.GetConnectionString("MainDb");
            var optionsBuilder = new DbContextOptionsBuilder<ProductCatalogDbContext>()
                .UseSqlServer(
                    connString ?? throw new InvalidOperationException(),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    }
                );

            Console.WriteLine(connString);
            return new ProductCatalogDbContext(optionsBuilder.Options);
        }
    }
}
﻿// <auto-generated />
using System;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoolStore.ProductCatalogApi.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ProductCatalogDbContext))]
    partial class ProductCatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoolStore.ProductCatalogApi.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Category","product");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80287ef3-987f-4312-a0c6-ccc2239aeea3"),
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Electronic Gadgets"
                        },
                        new
                        {
                            Id = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Food products"
                        },
                        new
                        {
                            Id = new Guid("1ebdd04f-a447-42a3-9e65-5697c1dacb09"),
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Accessories"
                        },
                        new
                        {
                            Id = new Guid("77666aa8-682c-4047-b075-04839281630a"),
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Beverage products"
                        });
                });

            modelBuilder.Entity("CoolStore.ProductCatalogApi.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product","product");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ba16da71-c7dd-4eac-9ead-5c2c2244e69f"),
                            CategoryId = new Guid("80287ef3-987f-4312-a0c6-ccc2239aeea3"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 243, DateTimeKind.Utc).AddTicks(2831),
                            Description = "IPhone 8",
                            ImageUrl = "https://picsum.photos/1200/900?image=1",
                            IsDeleted = false,
                            Name = "IPhone 8",
                            Price = 900.0,
                            StoreId = new Guid("90c9479e-a11c-4d6d-aaaa-0405b6c0efcd")
                        },
                        new
                        {
                            Id = new Guid("13d02035-2286-4055-ad2d-6855a60efbbb"),
                            CategoryId = new Guid("80287ef3-987f-4312-a0c6-ccc2239aeea3"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4267),
                            Description = "IPhone X",
                            ImageUrl = "https://picsum.photos/1200/900?image=2",
                            IsDeleted = false,
                            Name = "IPhone X",
                            Price = 1000.0,
                            StoreId = new Guid("90c9479e-a11c-4d6d-aaaa-0405b6c0efcd")
                        },
                        new
                        {
                            Id = new Guid("b8f0a771-339f-4602-a862-f7a51afd5b79"),
                            CategoryId = new Guid("80287ef3-987f-4312-a0c6-ccc2239aeea3"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4426),
                            Description = "MacBook Pro 2019",
                            ImageUrl = "https://picsum.photos/1200/900?image=3",
                            IsDeleted = false,
                            Name = "MacBook Pro 2019",
                            Price = 4000.0,
                            StoreId = new Guid("90c9479e-a11c-4d6d-aaaa-0405b6c0efcd")
                        },
                        new
                        {
                            Id = new Guid("ffd60654-1802-48bd-b4c3-d49831a8ab2c"),
                            CategoryId = new Guid("80287ef3-987f-4312-a0c6-ccc2239aeea3"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4455),
                            Description = "Asus UX370U i7 8550U (C4217TS)",
                            ImageUrl = "https://picsum.photos/1200/900?image=4",
                            IsDeleted = false,
                            Name = "Asus UX370U i7 8550U (C4217TS)",
                            Price = 500.0,
                            StoreId = new Guid("90c9479e-a11c-4d6d-aaaa-0405b6c0efcd")
                        },
                        new
                        {
                            Id = new Guid("a4811778-5070-4d70-8a9c-e6cb70dfcca4"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4526),
                            Description = "Mastoidectomy revision",
                            ImageUrl = "https://picsum.photos/1200/900?image=5",
                            IsDeleted = false,
                            Name = "Cheese - Camembert",
                            Price = 253.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("6a0e6d20-8bcc-450f-bc5c-b8f727083dcd"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4556),
                            Description = "Fiber-optic bronchoscopy",
                            ImageUrl = "https://picsum.photos/1200/900?image=6",
                            IsDeleted = false,
                            Name = "Bread - White, Unsliced",
                            Price = 2809.0,
                            StoreId = new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14")
                        },
                        new
                        {
                            Id = new Guid("fee1fc67-7469-4490-b418-47f4732de53f"),
                            CategoryId = new Guid("1ebdd04f-a447-42a3-9e65-5697c1dacb09"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4578),
                            Description = "Open periph nerve biopsy",
                            ImageUrl = "https://picsum.photos/1200/900?image=7",
                            IsDeleted = false,
                            Name = "Bag - Regular Kraft 20 Lb",
                            Price = 2147.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("2d2245e4-213a-49de-93d3-79e9439400f5"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4599),
                            Description = "Tibia/fibula inj op NOS",
                            ImageUrl = "https://picsum.photos/1200/900?image=8",
                            IsDeleted = false,
                            Name = "Wonton Wrappers",
                            Price = 2200.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("386b04c6-303a-4840-8a51-d92b1ea2d339"),
                            CategoryId = new Guid("77666aa8-682c-4047-b075-04839281630a"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4620),
                            Description = "Removal of FB NOS",
                            ImageUrl = "https://picsum.photos/1200/900?image=9",
                            IsDeleted = false,
                            Name = "Hersey Shakes",
                            Price = 2441.0,
                            StoreId = new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14")
                        },
                        new
                        {
                            Id = new Guid("297c5959-4808-4f40-8d6a-4a899505e1f7"),
                            CategoryId = new Guid("77666aa8-682c-4047-b075-04839281630a"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4643),
                            Description = "Remove bladder stimulat",
                            ImageUrl = "https://picsum.photos/1200/900?image=10",
                            IsDeleted = false,
                            Name = "Oranges - Navel, 72",
                            Price = 1731.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("3a0a0a89-9b3a-4046-bf2b-deee64a764d2"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4664),
                            Description = "Other bone dx proc NEC",
                            ImageUrl = "https://picsum.photos/1200/900?image=11",
                            IsDeleted = false,
                            Name = "Cheese - Swiss",
                            Price = 975.0,
                            StoreId = new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14")
                        },
                        new
                        {
                            Id = new Guid("71c46659-9560-4d6a-ac18-893477ed6662"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4685),
                            Description = "Oth thorac op thymus NOS",
                            ImageUrl = "https://picsum.photos/1200/900?image=12",
                            IsDeleted = false,
                            Name = "Lettuce - Boston Bib",
                            Price = 3453.0,
                            StoreId = new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14")
                        },
                        new
                        {
                            Id = new Guid("f92bfa6a-2522-4234-a7f1-9004596a4a85"),
                            CategoryId = new Guid("77666aa8-682c-4047-b075-04839281630a"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4706),
                            Description = "Toxicology-endocrine",
                            ImageUrl = "https://picsum.photos/1200/900?image=13",
                            IsDeleted = false,
                            Name = "Godiva White Chocolate",
                            Price = 2067.0,
                            StoreId = new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14")
                        },
                        new
                        {
                            Id = new Guid("cbe43158-2010-4cb1-a8de-f00da16a7362"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4727),
                            Description = "Opn/oth part gastrectomy",
                            ImageUrl = "https://picsum.photos/1200/900?image=14",
                            IsDeleted = false,
                            Name = "Ecolab - Balanced Fusion",
                            Price = 1769.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("85b9767c-1a09-4c33-8e77-faa25f1d69e1"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4749),
                            Description = "Tendon excision for grft",
                            ImageUrl = "https://picsum.photos/1200/900?image=15",
                            IsDeleted = false,
                            Name = "Tarragon - Primerba, Paste",
                            Price = 642.0,
                            StoreId = new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14")
                        },
                        new
                        {
                            Id = new Guid("22112bb2-c324-4860-8eb9-9c78853a52a5"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4769),
                            Description = "Vessel operation NEC",
                            ImageUrl = "https://picsum.photos/1200/900?image=16",
                            IsDeleted = false,
                            Name = "Beef - Tenderloin Tails",
                            Price = 967.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("97ad5bf4-d153-41c5-a6e0-6d0bfbbb4f67"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4819),
                            Description = "Skull plate removal",
                            ImageUrl = "https://picsum.photos/1200/900?image=17",
                            IsDeleted = false,
                            Name = "Oil - Olive",
                            Price = 1124.0,
                            StoreId = new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14")
                        },
                        new
                        {
                            Id = new Guid("cfc5cff8-ab2a-4c70-994d-5ab8ccb7cb0d"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4845),
                            Description = "Chng hnd mus/ten lng NEC",
                            ImageUrl = "https://picsum.photos/1200/900?image=18",
                            IsDeleted = false,
                            Name = "Crab - Dungeness, Whole, live",
                            Price = 1665.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("fac2ccc6-2c3f-4c1e-acbd-5354ba0873fb"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4866),
                            Description = "Plastic rep ext ear NEC",
                            ImageUrl = "https://picsum.photos/1200/900?image=19",
                            IsDeleted = false,
                            Name = "Pasta - Cappellini, Dry",
                            Price = 3305.0,
                            StoreId = new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14")
                        },
                        new
                        {
                            Id = new Guid("1adbc55a-4354-4205-b96d-c95e2dc806f4"),
                            CategoryId = new Guid("77666aa8-682c-4047-b075-04839281630a"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4887),
                            Description = "Interat ven retrn transp",
                            ImageUrl = "https://picsum.photos/1200/900?image=20",
                            IsDeleted = false,
                            Name = "Milk - Skim",
                            Price = 3310.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("c3770b10-dd0f-4b1c-83aa-d424c175c087"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4907),
                            Description = "Hepatic lobectomy",
                            ImageUrl = "https://picsum.photos/1200/900?image=20",
                            IsDeleted = false,
                            Name = "Beef - Shank",
                            Price = 3196.0,
                            StoreId = new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14")
                        },
                        new
                        {
                            Id = new Guid("6b8d0110-e3e8-4727-a51e-06f38864e464"),
                            CategoryId = new Guid("77666aa8-682c-4047-b075-04839281630a"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4929),
                            Description = "Proximal gastrectomy",
                            ImageUrl = "https://picsum.photos/1200/900?image=21",
                            IsDeleted = false,
                            Name = "Ice Cream Bar - Oreo Cone",
                            Price = 2236.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("3b69e116-9dd6-400f-96ce-9911f4f6ac8b"),
                            CategoryId = new Guid("77666aa8-682c-4047-b075-04839281630a"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4949),
                            Description = "Appendiceal ops NEC",
                            ImageUrl = "https://picsum.photos/1200/900?image=22",
                            IsDeleted = false,
                            Name = "Mix - Cocktail Ice Cream",
                            Price = 232.0,
                            StoreId = new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14")
                        },
                        new
                        {
                            Id = new Guid("89b46ea8-b9a6-40e5-8df3-dba1095695f7"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4972),
                            Description = "Excision of wrist NEC",
                            ImageUrl = "https://picsum.photos/1200/900?image=23",
                            IsDeleted = false,
                            Name = "Mushroom - Lg - Cello",
                            Price = 3318.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("e88e07f8-358d-48f7-b17c-8a16458f9c0a"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(4993),
                            Description = "Oth chest cage rep/plast",
                            ImageUrl = "https://picsum.photos/1200/900?image=24",
                            IsDeleted = false,
                            Name = "Lotus Leaves",
                            Price = 1504.0,
                            StoreId = new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14")
                        },
                        new
                        {
                            Id = new Guid("b243a35d-120a-4db3-ad12-7b3fa80e6391"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(5014),
                            Description = "Implt/repl carddefib tot",
                            ImageUrl = "https://picsum.photos/1200/900?image=25",
                            IsDeleted = false,
                            Name = "Soup - Campbells Chili",
                            Price = 3294.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("6e3ac253-517d-48e5-96ad-800451f8591c"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(5034),
                            Description = "Wound catheter irrigat",
                            ImageUrl = "https://picsum.photos/1200/900?image=26",
                            IsDeleted = false,
                            Name = "Longos - Penne With Pesto",
                            Price = 3639.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        },
                        new
                        {
                            Id = new Guid("4693520a-2b14-4d90-8b64-541575511382"),
                            CategoryId = new Guid("664690ee-a647-4b12-b87f-af5c511187eb"),
                            Created = new DateTime(2020, 4, 30, 4, 26, 9, 246, DateTimeKind.Utc).AddTicks(5055),
                            Description = "Abdomen wall repair NEC",
                            ImageUrl = "https://picsum.photos/1200/900?image=27",
                            IsDeleted = false,
                            Name = "Prunes - Pitted",
                            Price = 1191.0,
                            StoreId = new Guid("b8b62196-6369-409d-b709-11c112dd023f")
                        });
                });

            modelBuilder.Entity("CoolStore.ProductCatalogApi.Domain.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Rating","product");
                });

            modelBuilder.Entity("CoolStore.ProductCatalogApi.Domain.Product", b =>
                {
                    b.HasOne("CoolStore.ProductCatalogApi.Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

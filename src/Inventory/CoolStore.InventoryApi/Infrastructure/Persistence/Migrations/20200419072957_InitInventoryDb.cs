using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoolStore.InventoryApi.Infrastructure.Persistence.Migrations
{
    public partial class InitInventoryDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "inv");

            migrationBuilder.CreateTable(
                name: "Inventory",
                schema: "inv",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    Location = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "inv",
                table: "Inventory",
                columns: new[] { "Id", "Created", "Description", "Location", "Updated", "Website" },
                values: new object[] { new Guid("90c9479e-a11c-4d6d-aaaa-0405b6c0efcd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This store sells electronic gadgets", "Vietnam", null, "https://coolstore-vn.com" });

            migrationBuilder.InsertData(
                schema: "inv",
                table: "Inventory",
                columns: new[] { "Id", "Created", "Description", "Location", "Updated", "Website" },
                values: new object[] { new Guid("b8b62196-6369-409d-b709-11c112dd023f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This store sells food and beverage products", "Seattle", null, "https://coolstore-sea.com" });

            migrationBuilder.InsertData(
                schema: "inv",
                table: "Inventory",
                columns: new[] { "Id", "Created", "Description", "Location", "Updated", "Website" },
                values: new object[] { new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This store sells food and beverage products", "San Francisco", null, "https://coolstore-san.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory",
                schema: "inv");
        }
    }
}

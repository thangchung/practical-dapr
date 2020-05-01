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
                name: "Store",
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
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreProductPrice",
                schema: "inv",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    StoreId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Rop = table.Column<int>(nullable: false),
                    Eoq = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProductPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProductPrice_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "inv",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "inv",
                table: "Store",
                columns: new[] { "Id", "Created", "Description", "Location", "Updated", "Website" },
                values: new object[] { new Guid("90c9479e-a11c-4d6d-aaaa-0405b6c0efcd"), new DateTime(2020, 5, 1, 14, 15, 33, 90, DateTimeKind.Utc).AddTicks(6170), "This store sells electronic gadgets", "Vietnam", null, "https://coolstore-vn.com" });

            migrationBuilder.InsertData(
                schema: "inv",
                table: "Store",
                columns: new[] { "Id", "Created", "Description", "Location", "Updated", "Website" },
                values: new object[] { new Guid("b8b62196-6369-409d-b709-11c112dd023f"), new DateTime(2020, 5, 1, 14, 15, 33, 94, DateTimeKind.Utc).AddTicks(4559), "This store sells food and beverage products", "Seattle", null, "https://coolstore-sea.com" });

            migrationBuilder.InsertData(
                schema: "inv",
                table: "Store",
                columns: new[] { "Id", "Created", "Description", "Location", "Updated", "Website" },
                values: new object[] { new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), new DateTime(2020, 5, 1, 14, 15, 33, 94, DateTimeKind.Utc).AddTicks(4759), "This store sells food and beverage products", "San Francisco", null, "https://coolstore-san.com" });

            migrationBuilder.InsertData(
                schema: "inv",
                table: "StoreProductPrice",
                columns: new[] { "Id", "Created", "Eoq", "Price", "ProductId", "Rop", "StoreId", "Updated" },
                values: new object[,]
                {
                    { new Guid("785ec0d4-cdb2-480b-b8b3-2f65950ef048"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(7773), 10, 900.0, new Guid("ba16da71-c7dd-4eac-9ead-5c2c2244e69f"), 5, new Guid("90c9479e-a11c-4d6d-aaaa-0405b6c0efcd"), null },
                    { new Guid("3dbe06af-c15d-4cfa-9f43-590c001063a6"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(9123), 10, 3196.0, new Guid("c3770b10-dd0f-4b1c-83aa-d424c175c087"), 5, new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), null },
                    { new Guid("ba855106-3eb1-4be9-a565-ccf2ad5270a1"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(9057), 10, 3305.0, new Guid("fac2ccc6-2c3f-4c1e-acbd-5354ba0873fb"), 5, new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), null },
                    { new Guid("9ba82d65-0830-49e9-8d7d-f309ed42457b"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8987), 10, 1124.0, new Guid("97ad5bf4-d153-41c5-a6e0-6d0bfbbb4f67"), 5, new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), null },
                    { new Guid("13dd993e-d979-4e22-a159-f577dffb8062"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8920), 10, 642.0, new Guid("85b9767c-1a09-4c33-8e77-faa25f1d69e1"), 5, new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), null },
                    { new Guid("325436f9-f674-4fbb-bbf5-f2a7c813be2d"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8853), 10, 2067.0, new Guid("f92bfa6a-2522-4234-a7f1-9004596a4a85"), 5, new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), null },
                    { new Guid("d8dda9ba-ae9f-4c14-8980-9b6bdf12d66d"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8820), 10, 3453.0, new Guid("71c46659-9560-4d6a-ac18-893477ed6662"), 5, new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), null },
                    { new Guid("56a082ac-3e9b-4702-848c-2ba75400ff3a"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8787), 10, 975.0, new Guid("3a0a0a89-9b3a-4046-bf2b-deee64a764d2"), 5, new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), null },
                    { new Guid("93c6ba3e-e4cb-40a2-89bc-c8550c672237"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8655), 10, 2441.0, new Guid("386b04c6-303a-4840-8a51-d92b1ea2d339"), 5, new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), null },
                    { new Guid("37f5b3a2-557e-4fe8-b5e3-8d581a237583"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8556), 10, 2809.0, new Guid("6a0e6d20-8bcc-450f-bc5c-b8f727083dcd"), 5, new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), null },
                    { new Guid("726eb225-a323-479c-82b6-4165532d1be4"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(9388), 10, 1191.0, new Guid("4693520a-2b14-4d90-8b64-541575511382"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("832587a7-a37f-4efd-b99f-a89451397d96"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(9355), 10, 3639.0, new Guid("6e3ac253-517d-48e5-96ad-800451f8591c"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("d00b6019-5c1d-4317-8102-9482bc90e3bd"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(9323), 10, 3294.0, new Guid("b243a35d-120a-4db3-ad12-7b3fa80e6391"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("585e4160-ce70-4e78-b1e6-e4a9aa9d91b0"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(9257), 10, 3318.0, new Guid("89b46ea8-b9a6-40e5-8df3-dba1095695f7"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("df0db461-720b-4b4d-958e-e1178fdd4fa9"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(9156), 10, 2236.0, new Guid("6b8d0110-e3e8-4727-a51e-06f38864e464"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("79292e85-43e2-40a0-969a-da8a9987de8a"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(9089), 10, 3310.0, new Guid("1adbc55a-4354-4205-b96d-c95e2dc806f4"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("08eb6d70-b4f7-4eac-8cd5-f4856cc751f3"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(9023), 10, 1665.0, new Guid("cfc5cff8-ab2a-4c70-994d-5ab8ccb7cb0d"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("58436d6f-9c16-445f-b70d-b4159ba0fa56"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8953), 10, 967.0, new Guid("22112bb2-c324-4860-8eb9-9c78853a52a5"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("9e3a2db9-6345-49e6-b22b-3e0aaeec0923"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8886), 10, 1769.0, new Guid("cbe43158-2010-4cb1-a8de-f00da16a7362"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("3f4db00c-67c7-43d6-9ffc-736877a7413e"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8751), 10, 1731.0, new Guid("297c5959-4808-4f40-8d6a-4a899505e1f7"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("f643880d-7153-4ee2-b66b-9bd1e466043c"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8621), 10, 2200.0, new Guid("2d2245e4-213a-49de-93d3-79e9439400f5"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("ba26b27c-1419-4e5c-a0ed-c98c4ec2a1d9"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8589), 10, 2147.0, new Guid("fee1fc67-7469-4490-b418-47f4732de53f"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("a38c9b35-6b4e-46c1-8864-ea03310e9bb1"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8517), 10, 253.0, new Guid("a4811778-5070-4d70-8a9c-e6cb70dfcca4"), 5, new Guid("b8b62196-6369-409d-b709-11c112dd023f"), null },
                    { new Guid("776d9751-2fc3-4aa6-94e5-83ac8c30c0e5"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8483), 10, 500.0, new Guid("ffd60654-1802-48bd-b4c3-d49831a8ab2c"), 5, new Guid("90c9479e-a11c-4d6d-aaaa-0405b6c0efcd"), null },
                    { new Guid("55434919-47da-411b-a11d-c664650e9565"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8445), 10, 4000.0, new Guid("b8f0a771-339f-4602-a862-f7a51afd5b79"), 5, new Guid("90c9479e-a11c-4d6d-aaaa-0405b6c0efcd"), null },
                    { new Guid("ce8014b6-e8ed-49e6-a1bf-4a73c62fcbf6"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(8390), 10, 1000.0, new Guid("13d02035-2286-4055-ad2d-6855a60efbbb"), 5, new Guid("90c9479e-a11c-4d6d-aaaa-0405b6c0efcd"), null },
                    { new Guid("c294478a-e537-4cf9-b36d-acc3458e5df1"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(9223), 10, 232.0, new Guid("3b69e116-9dd6-400f-96ce-9911f4f6ac8b"), 5, new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), null },
                    { new Guid("bb9d2a1f-f17d-40a4-b66b-128ec29fff6c"), new DateTime(2020, 5, 1, 14, 15, 33, 121, DateTimeKind.Utc).AddTicks(9290), 10, 1504.0, new Guid("e88e07f8-358d-48f7-b17c-8a16458f9c0a"), 5, new Guid("ec186ddf-f430-44ec-84e5-205c93d84f14"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreProductPrice_StoreId",
                schema: "inv",
                table: "StoreProductPrice",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreProductPrice",
                schema: "inv");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "inv");
        }
    }
}

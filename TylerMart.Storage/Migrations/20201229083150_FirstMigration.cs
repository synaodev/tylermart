#pragma warning disable 1591

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TylerMart.Storage.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Complete = table.Column<bool>(type: "bit", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LocationProducts_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "EmailAddress", "FirstName", "LastName", "Password", "RealAddress" },
                values: new object[] { 1, "admin.admin@revature.net", "Admin", "Admin", "administrator", "Nowhere" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Dreamland" },
                    { 2, "California" },
                    { 3, "Washington" },
                    { 4, "Oregon" },
                    { 5, "Texas" },
                    { 6, "New York" },
                    { 7, "Virginia" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "This is a nightmare!", "Nightmare", 180.50m },
                    { 2, "Delicious eggs", "Eggs", 4.40m },
                    { 3, "For storing books", "Bookshelf", 68.99m },
                    { 4, "For when it's chilly outside", "Jacket", 18.99m },
                    { 5, "A bunch of oranges", "Oranges", 4.0m },
                    { 6, "It's a purple-ish color", "Lipstick", 5.99m },
                    { 7, "An excellent game", "Cave Story Switch", 35.99m }
                });

            migrationBuilder.InsertData(
                table: "LocationProducts",
                columns: new[] { "ID", "LocationID", "ProductID" },
                values: new object[,]
                {
                    { 18, 6, 3 },
                    { 19, 6, 7 },
                    { 12, 4, 7 },
                    { 26, 7, 6 },
                    { 11, 4, 6 },
                    { 25, 7, 5 },
                    { 21, 6, 5 },
                    { 10, 4, 5 },
                    { 24, 7, 4 },
                    { 9, 3, 4 },
                    { 8, 3, 4 },
                    { 7, 3, 4 },
                    { 23, 7, 3 },
                    { 27, 7, 7 },
                    { 17, 6, 3 },
                    { 6, 2, 3 },
                    { 5, 2, 3 },
                    { 4, 2, 3 },
                    { 22, 7, 2 },
                    { 16, 5, 2 },
                    { 15, 5, 2 },
                    { 14, 5, 2 },
                    { 13, 5, 2 },
                    { 3, 2, 2 },
                    { 2, 2, 2 },
                    { 1, 1, 1 },
                    { 20, 6, 7 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "ID", "Complete", "CreatedAt", "CustomerID", "LocationID" },
                values: new object[] { 1, true, new DateTime(2020, 12, 29, 0, 31, 49, 607, DateTimeKind.Local).AddTicks(1305), 1, 1 });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "ID", "OrderID", "ProductID" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EmailAddress",
                table: "Customers",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationProducts_LocationID",
                table: "LocationProducts",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationProducts_ProductID",
                table: "LocationProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Name",
                table: "Locations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderID",
                table: "OrderProducts",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductID",
                table: "OrderProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LocationID",
                table: "Orders",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationProducts");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}

#pragma warning restore 1591

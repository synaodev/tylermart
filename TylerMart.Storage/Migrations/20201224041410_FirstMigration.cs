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
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlacedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationProducts",
                columns: table => new
                {
                    LocationProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationProducts", x => x.LocationProductID);
                    table.ForeignKey(
                        name: "FK_LocationProducts_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.OrderProductID);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "EmailAddress", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1, "tyler.cadena@revature.net", "Tyler", "Cadena", "synaodev" },
                    { 2, "george.bumble@revature.net", "George", "Bumble", "onionbutt" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "Name" },
                values: new object[,]
                {
                    { 1, "New Jersey" },
                    { 2, "Florida" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "You can carry stuff around", "Bag", 3.50m },
                    { 2, "Helpful for those with poor eyesight", "Glasses", 20.00m }
                });

            migrationBuilder.InsertData(
                table: "LocationProducts",
                columns: new[] { "LocationProductID", "LocationID", "ProductID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 },
                    { 5, 2, 1 },
                    { 6, 2, 1 },
                    { 3, 1, 2 },
                    { 4, 1, 2 },
                    { 7, 2, 2 },
                    { 8, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "Completed", "CustomerID", "LocationID", "PlacedAt" },
                values: new object[,]
                {
                    { 1, false, 1, 1, new DateTime(2020, 12, 23, 20, 14, 10, 15, DateTimeKind.Local).AddTicks(3107) },
                    { 2, false, 2, 2, new DateTime(2020, 12, 23, 20, 14, 10, 19, DateTimeKind.Local).AddTicks(8238) }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderProductID", "OrderID", "ProductID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 1 },
                    { 4, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EmailAddress",
                table: "Customers",
                column: "EmailAddress",
                unique: true,
                filter: "[EmailAddress] IS NOT NULL");

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
                unique: true,
                filter: "[Name] IS NOT NULL");

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

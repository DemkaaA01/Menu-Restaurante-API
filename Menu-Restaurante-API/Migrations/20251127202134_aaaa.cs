using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Menu_Restaurante_API.Migrations
{
    /// <inheritdoc />
    public partial class aaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Visits = table.Column<int>(type: "INTEGER", nullable: false),
                    RestaurantName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DiscountPercent = table.Column<int>(type: "INTEGER", nullable: false),
                    HappyHourEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    HappyHourStart = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    HappyHourEnd = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "Password", "Phone", "RestaurantName", "Username", "Visits" },
                values: new object[,]
                {
                    { 1, "", "sanmartin@gmail.com", "12345", "", "", "Restaurante San Martín", 0 },
                    { 2, "", "pizzeria@gmail.com", "abcd1234", "", "", "Pizzería El Rincón", 0 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "IsActive", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "", true, "Bebidas", 1 },
                    { 2, "", true, "Postres", 1 },
                    { 3, "", true, "Platos Principales", 1 },
                    { 4, "", true, "Pizzas", 2 },
                    { 5, "", true, "Empanadas", 2 },
                    { 6, "", true, "Bebidas", 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "DiscountPercent", "HappyHourEnabled", "HappyHourEnd", "HappyHourStart", "Name", "Price", "UserId" },
                values: new object[,]
                {
                    { 1, 3, "Clásica milanesa con papas fritas doradas", 0, false, null, null, "Milanesa con papas fritas", 2500m, 1 },
                    { 2, 2, "Helado artesanal de chocolate amargo", 10, true, null, null, "Helado de chocolate", 900m, 1 },
                    { 3, 1, "IPA rubia elaborada localmente", 20, true, null, null, "Cerveza artesanal 500ml", 1200m, 1 },
                    { 4, 4, "Mozzarella, tomate y albahaca fresca", 0, false, null, null, "Pizza Margarita", 1800m, 2 },
                    { 5, 5, "Empanada jugosa con carne cortada a cuchillo", 0, false, null, null, "Empanada de Carne", 350m, 2 },
                    { 6, 6, "Botella de Coca-Cola 1.5 litros", 30, true, null, null, "Gaseosa Cola 1.5L", 800m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

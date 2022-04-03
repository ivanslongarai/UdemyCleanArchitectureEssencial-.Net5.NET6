using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchMvc.Infra.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)

        {

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Name", "Description", "Price", "Stock", "Image", "CategoryId", "CreatedAt", "ModifiedAt" },
                values: new object[] 
                    { 
                        "Notebook",
                        "Special nootebook",
                        5.50,
                        70,
                        "img1.jpg",
                        1,
                        new DateTime(2022, 3, 31, 20, 15, 54, 306, DateTimeKind.Local).AddTicks(4996), 
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)});

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Name", "Description", "Price", "Stock", "Image", "CategoryId", "CreatedAt", "ModifiedAt" },
                values: new object[]
                    {
                        "Pen",
                        "New pen",
                        4.50,
                        30,
                        "img2.jpg",
                        2,
                        new DateTime(2022, 3, 31, 20, 15, 54, 306, DateTimeKind.Local).AddTicks(4996),
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)});

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Name", "Description", "Price", "Stock", "Image", "CategoryId", "CreatedAt", "ModifiedAt" },
                values: new object[]
                    {
                        "Calculator",
                        "Calculator 2.0",
                        25.50,
                        12,
                        "img3.jpg",
                        2,
                        new DateTime(2022, 3, 31, 20, 15, 54, 306, DateTimeKind.Local).AddTicks(4996),
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)});

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Name", "Description", "Price", "Stock", "Image", "CategoryId", "CreatedAt", "ModifiedAt" },
                values: new object[]
                    {
                        "Table",
                        "Small table",
                        122.50,
                        15,
                        "img1.jpg",
                        3,
                        new DateTime(2022, 3, 31, 20, 15, 54, 306, DateTimeKind.Local).AddTicks(4996),
                        new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)});         

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Products]");
        }
    }
}

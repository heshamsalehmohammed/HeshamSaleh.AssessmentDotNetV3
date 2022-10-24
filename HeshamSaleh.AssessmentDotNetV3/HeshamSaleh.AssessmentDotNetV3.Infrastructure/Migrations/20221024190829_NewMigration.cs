using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeshamSaleh.AssessmentDotNetV3.Infrastructure.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    ImgURL = table.Column<string>(nullable: true),
                    CateogryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), "Commandeering a Ship Without Getting Caught" },
                    { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), "Overthrowing Mutiny" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CateogryId", "ImgURL", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), "//", "Short", "25EGP", 5 },
                    { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), "//", "T-Shirt", "25EGP", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}

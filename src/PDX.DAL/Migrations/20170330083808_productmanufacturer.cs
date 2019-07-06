using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDX.DAL.Migrations
{
    public partial class productmanufacturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productmanufacturer",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    manufacturer_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productmanufacturer", x => x.id);
                    table.ForeignKey(
                        name: "FK_productmanufacturer_manufacturer_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalSchema: "customer",
                        principalTable: "manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productmanufacturer_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "commodity",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productmanufacturer_manufacturer_id",
                schema: "commodity",
                table: "productmanufacturer",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "IX_productmanufacturer_product_id",
                schema: "commodity",
                table: "productmanufacturer",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productmanufacturer",
                schema: "commodity");
        }
    }
}

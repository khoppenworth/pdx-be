using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDX.DAL.Migrations
{
    public partial class AgentLevelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_agentproduct_agent_agent_id",
                schema: "commodity",
                table: "agentproduct");

            migrationBuilder.DropForeignKey(
                name: "FK_agentproduct_product_product_id",
                schema: "commodity",
                table: "agentproduct");

            migrationBuilder.DropForeignKey(
                name: "FK_productmanufacturer_manufacturer_manufacturer_id",
                schema: "commodity",
                table: "productmanufacturer");

            migrationBuilder.DropForeignKey(
                name: "FK_productmanufacturer_product_product_id",
                schema: "commodity",
                table: "productmanufacturer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productmanufacturer",
                schema: "commodity",
                table: "productmanufacturer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_agentproduct",
                schema: "commodity",
                table: "agentproduct");

            migrationBuilder.RenameTable(
                name: "productmanufacturer",
                schema: "commodity",
                newName: "product_manufacturer");

            migrationBuilder.RenameTable(
                name: "agentproduct",
                schema: "commodity",
                newName: "agent_product");

            migrationBuilder.RenameIndex(
                name: "IX_productmanufacturer_product_id",
                schema: "commodity",
                table: "product_manufacturer",
                newName: "IX_product_manufacturer_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_productmanufacturer_manufacturer_id",
                schema: "commodity",
                table: "product_manufacturer",
                newName: "IX_product_manufacturer_manufacturer_id");

            migrationBuilder.RenameIndex(
                name: "IX_agentproduct_product_id",
                schema: "commodity",
                table: "agent_product",
                newName: "IX_agent_product_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_agentproduct_agent_id",
                schema: "commodity",
                table: "agent_product",
                newName: "IX_agent_product_agent_id");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "customer",
                table: "manufacturer",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "commodity",
                table: "product",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "generic_name",
                schema: "commodity",
                table: "product",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_manufacturer",
                schema: "commodity",
                table: "product_manufacturer",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_agent_product",
                schema: "commodity",
                table: "agent_product",
                column: "id");

            migrationBuilder.CreateTable(
                name: "agent_level",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_level_code = table.Column<string>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agent_level", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "agent_supplier",
                schema: "customer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_id = table.Column<int>(nullable: false),
                    agent_level_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    supplier_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agent_supplier", x => x.id);
                    table.ForeignKey(
                        name: "FK_agent_supplier_agent_agent_id",
                        column: x => x.agent_id,
                        principalSchema: "customer",
                        principalTable: "agent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_agent_supplier_agent_level_agent_level_id",
                        column: x => x.agent_level_id,
                        principalSchema: "common",
                        principalTable: "agent_level",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_agent_supplier_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "customer",
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agent_supplier_agent_id",
                schema: "customer",
                table: "agent_supplier",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_agent_supplier_agent_level_id",
                schema: "customer",
                table: "agent_supplier",
                column: "agent_level_id");

            migrationBuilder.CreateIndex(
                name: "IX_agent_supplier_supplier_id",
                schema: "customer",
                table: "agent_supplier",
                column: "supplier_id");

            migrationBuilder.AddForeignKey(
                name: "FK_agent_product_agent_agent_id",
                schema: "commodity",
                table: "agent_product",
                column: "agent_id",
                principalSchema: "customer",
                principalTable: "agent",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_agent_product_product_product_id",
                schema: "commodity",
                table: "agent_product",
                column: "product_id",
                principalSchema: "commodity",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_manufacturer_manufacturer_manufacturer_id",
                schema: "commodity",
                table: "product_manufacturer",
                column: "manufacturer_id",
                principalSchema: "customer",
                principalTable: "manufacturer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_manufacturer_product_product_id",
                schema: "commodity",
                table: "product_manufacturer",
                column: "product_id",
                principalSchema: "commodity",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_agent_product_agent_agent_id",
                schema: "commodity",
                table: "agent_product");

            migrationBuilder.DropForeignKey(
                name: "FK_agent_product_product_product_id",
                schema: "commodity",
                table: "agent_product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_manufacturer_manufacturer_manufacturer_id",
                schema: "commodity",
                table: "product_manufacturer");

            migrationBuilder.DropForeignKey(
                name: "FK_product_manufacturer_product_product_id",
                schema: "commodity",
                table: "product_manufacturer");

            migrationBuilder.DropTable(
                name: "agent_supplier",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "agent_level",
                schema: "common");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_manufacturer",
                schema: "commodity",
                table: "product_manufacturer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_agent_product",
                schema: "commodity",
                table: "agent_product");

            migrationBuilder.RenameTable(
                name: "product_manufacturer",
                schema: "commodity",
                newName: "productmanufacturer");

            migrationBuilder.RenameTable(
                name: "agent_product",
                schema: "commodity",
                newName: "agentproduct");

            migrationBuilder.RenameIndex(
                name: "IX_product_manufacturer_product_id",
                schema: "commodity",
                table: "productmanufacturer",
                newName: "IX_productmanufacturer_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_product_manufacturer_manufacturer_id",
                schema: "commodity",
                table: "productmanufacturer",
                newName: "IX_productmanufacturer_manufacturer_id");

            migrationBuilder.RenameIndex(
                name: "IX_agent_product_product_id",
                schema: "commodity",
                table: "agentproduct",
                newName: "IX_agentproduct_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_agent_product_agent_id",
                schema: "commodity",
                table: "agentproduct",
                newName: "IX_agentproduct_agent_id");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "customer",
                table: "manufacturer",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "commodity",
                table: "product",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "generic_name",
                schema: "commodity",
                table: "product",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300);

            migrationBuilder.AddPrimaryKey(
                name: "PK_productmanufacturer",
                schema: "commodity",
                table: "productmanufacturer",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_agentproduct",
                schema: "commodity",
                table: "agentproduct",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_agentproduct_agent_agent_id",
                schema: "commodity",
                table: "agentproduct",
                column: "agent_id",
                principalSchema: "customer",
                principalTable: "agent",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_agentproduct_product_product_id",
                schema: "commodity",
                table: "agentproduct",
                column: "product_id",
                principalSchema: "commodity",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productmanufacturer_manufacturer_manufacturer_id",
                schema: "commodity",
                table: "productmanufacturer",
                column: "manufacturer_id",
                principalSchema: "customer",
                principalTable: "manufacturer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productmanufacturer_product_product_id",
                schema: "commodity",
                table: "productmanufacturer",
                column: "product_id",
                principalSchema: "commodity",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

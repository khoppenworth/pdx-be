using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDX.DAL.Migrations
{
    public partial class AgentProductMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agentproduct",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agentproduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_agentproduct_agent_agent_id",
                        column: x => x.agent_id,
                        principalSchema: "customer",
                        principalTable: "agent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_agentproduct_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "commodity",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agentproduct_agent_id",
                schema: "commodity",
                table: "agentproduct",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_agentproduct_product_id",
                schema: "commodity",
                table: "agentproduct",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agentproduct",
                schema: "commodity");
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDX.DAL.Migrations
{
    public partial class LettersMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "letter",
                schema: "document",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Body = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    Footer = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    module_document_id = table.Column<int>(nullable: false),
                    OtherText = table.Column<string>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_letter", x => x.id);
                    table.ForeignKey(
                        name: "FK_letter_module_document_module_document_id",
                        column: x => x.module_document_id,
                        principalSchema: "document",
                        principalTable: "module_document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "letter_heading",
                schema: "document",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CompanyName = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    LogoUrl = table.Column<string>(nullable: true),
                    modified_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_letter_heading", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_letter_module_document_id",
                schema: "document",
                table: "letter",
                column: "module_document_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "letter",
                schema: "document");

            migrationBuilder.DropTable(
                name: "letter_heading",
                schema: "document");
        }
    }
}

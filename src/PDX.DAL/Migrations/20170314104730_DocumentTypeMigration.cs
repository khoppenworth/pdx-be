using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PDX.DAL.Migrations
{
    public partial class DocumentTypeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "document_type_code",
                schema: "common",
                table: "document_type");

            migrationBuilder.RenameColumn(
                name: "direction",
                schema: "common",
                table: "document_type",
                newName: "is_system_generated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_system_generated",
                schema: "common",
                table: "document_type",
                newName: "direction");

            migrationBuilder.AddColumn<string>(
                name: "document_type_code",
                schema: "common",
                table: "document_type",
                nullable: false,
                defaultValue: "");
        }
    }
}

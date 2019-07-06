using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PDX.DAL.Migrations
{
    public partial class AssignIPermitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "assigned_user_id",
                schema: "procurement",
                table: "import_permit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "currency",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_assigned_user_id",
                schema: "procurement",
                table: "import_permit",
                column: "assigned_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_import_permit_user_assigned_user_id",
                schema: "procurement",
                table: "import_permit",
                column: "assigned_user_id",
                principalSchema: "account",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_import_permit_user_assigned_user_id",
                schema: "procurement",
                table: "import_permit");

            migrationBuilder.DropIndex(
                name: "IX_import_permit_assigned_user_id",
                schema: "procurement",
                table: "import_permit");

            migrationBuilder.DropColumn(
                name: "assigned_user_id",
                schema: "procurement",
                table: "import_permit");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "currency");
        }
    }
}

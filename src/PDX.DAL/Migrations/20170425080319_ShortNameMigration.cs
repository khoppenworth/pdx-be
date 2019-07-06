using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PDX.DAL.Migrations
{
    public partial class ShortNameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_permission_module_module_id",
                schema: "account",
                table: "role_permission");

            migrationBuilder.DropIndex(
                name: "IX_role_permission_module_id",
                schema: "account",
                table: "role_permission");

            migrationBuilder.DropColumn(
                name: "module_id",
                schema: "account",
                table: "role_permission");

            migrationBuilder.AddColumn<int>(
                name: "supplier_id",
                schema: "procurement",
                table: "import_permit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "user_type",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "shipping_method",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "port_of_entry",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "payment_mode",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "module",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "import_permit_type",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "import_permit_status",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "import_permit_delivery",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "document_type",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "commodity_type",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "agent_type",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "common",
                table: "agent_level",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_supplier_id",
                schema: "procurement",
                table: "import_permit",
                column: "supplier_id");

            migrationBuilder.AddForeignKey(
                name: "FK_import_permit_supplier_supplier_id",
                schema: "procurement",
                table: "import_permit",
                column: "supplier_id",
                principalSchema: "customer",
                principalTable: "supplier",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_import_permit_supplier_supplier_id",
                schema: "procurement",
                table: "import_permit");

            migrationBuilder.DropIndex(
                name: "IX_import_permit_supplier_id",
                schema: "procurement",
                table: "import_permit");

            migrationBuilder.DropColumn(
                name: "supplier_id",
                schema: "procurement",
                table: "import_permit");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "user_type");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "shipping_method");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "port_of_entry");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "payment_mode");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "module");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "import_permit_type");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "import_permit_status");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "import_permit_delivery");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "document_type");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "commodity_type");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "agent_type");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "common",
                table: "agent_level");

            migrationBuilder.AddColumn<int>(
                name: "module_id",
                schema: "account",
                table: "role_permission",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_module_id",
                schema: "account",
                table: "role_permission",
                column: "module_id");

            migrationBuilder.AddForeignKey(
                name: "FK_role_permission_module_module_id",
                schema: "account",
                table: "role_permission",
                column: "module_id",
                principalSchema: "common",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

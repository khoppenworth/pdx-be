using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDX.DAL.Migrations
{
    public partial class IpermitColumnsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fee_received",
                schema: "procurement",
                table: "import_permit");

            migrationBuilder.DropColumn(
                name: "fee_received_date",
                schema: "procurement",
                table: "import_permit");

            migrationBuilder.DropColumn(
                name: "gdt_id",
                schema: "procurement",
                table: "import_permit");

            migrationBuilder.AddColumn<int>(
                name: "shipping_method_id",
                schema: "common",
                table: "port_of_entry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "vwimport_permit",
                schema: "procurement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_id = table.Column<int>(nullable: false),
                    agent_name = table.Column<string>(nullable: true),
                    amount = table.Column<decimal>(nullable: false),
                    created_by_user_id = table.Column<int>(nullable: false),
                    created_by_username = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    currency = table.Column<string>(nullable: true),
                    currency_id = table.Column<int>(nullable: false),
                    delivery = table.Column<string>(nullable: true),
                    expiry_date = table.Column<DateTime>(nullable: true),
                    freight_cost = table.Column<decimal>(nullable: false),
                    import_permit_number = table.Column<string>(nullable: true),
                    import_permit_status = table.Column<string>(nullable: true),
                    import_permit_status_code = table.Column<string>(nullable: true),
                    import_permit_status_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    payment_mode = table.Column<string>(nullable: true),
                    payment_mode_id = table.Column<int>(nullable: false),
                    performa_invoice_number = table.Column<string>(nullable: true),
                    port_of_entry = table.Column<string>(nullable: true),
                    port_of_entry_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    shipping_method = table.Column<string>(nullable: true),
                    shipping_method_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vwimport_permit", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_port_of_entry_shipping_method_id",
                schema: "common",
                table: "port_of_entry",
                column: "shipping_method_id");

            migrationBuilder.AddForeignKey(
                name: "FK_port_of_entry_shipping_method_shipping_method_id",
                schema: "common",
                table: "port_of_entry",
                column: "shipping_method_id",
                principalSchema: "common",
                principalTable: "shipping_method",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_port_of_entry_shipping_method_shipping_method_id",
                schema: "common",
                table: "port_of_entry");

            migrationBuilder.DropTable(
                name: "vwimport_permit",
                schema: "procurement");

            migrationBuilder.DropIndex(
                name: "IX_port_of_entry_shipping_method_id",
                schema: "common",
                table: "port_of_entry");

            migrationBuilder.DropColumn(
                name: "shipping_method_id",
                schema: "common",
                table: "port_of_entry");

            migrationBuilder.AddColumn<bool>(
                name: "fee_received",
                schema: "procurement",
                table: "import_permit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "fee_received_date",
                schema: "procurement",
                table: "import_permit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gdt_id",
                schema: "procurement",
                table: "import_permit",
                nullable: true);
        }
    }
}

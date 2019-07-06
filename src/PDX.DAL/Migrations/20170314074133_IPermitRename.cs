using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDX.DAL.Migrations
{
    public partial class IPermitRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchase_order_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "purchase_order_detail",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "purchase_order_log_status",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "purchase_order",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "purchase_order_status",
                schema: "common");

            migrationBuilder.CreateTable(
                name: "import_permit_status",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    import_permit_status_code = table.Column<string>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_import_permit_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "import_permit_type",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    import_permit_type_code = table.Column<string>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_import_permit_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "import_permit",
                schema: "procurement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_id = table.Column<int>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    created_by_user_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    currency_id = table.Column<int>(nullable: false),
                    delivery = table.Column<string>(maxLength: 100, nullable: false),
                    expiry_date = table.Column<DateTime>(nullable: true),
                    fee_received = table.Column<bool>(nullable: false),
                    fee_received_date = table.Column<DateTime>(nullable: true),
                    freight_cost = table.Column<decimal>(nullable: false),
                    gdt_id = table.Column<string>(nullable: true),
                    import_permit_number = table.Column<string>(maxLength: 100, nullable: false),
                    import_permit_status_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    payment_mode_id = table.Column<int>(nullable: false),
                    performa_invoice_number = table.Column<string>(nullable: true),
                    port_of_entry_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    shipping_method_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_import_permit", x => x.id);
                    table.ForeignKey(
                        name: "FK_import_permit_agent_agent_id",
                        column: x => x.agent_id,
                        principalSchema: "customer",
                        principalTable: "agent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_import_permit_user_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_import_permit_currency_currency_id",
                        column: x => x.currency_id,
                        principalSchema: "common",
                        principalTable: "currency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_import_permit_import_permit_status_import_permit_status_id",
                        column: x => x.import_permit_status_id,
                        principalSchema: "common",
                        principalTable: "import_permit_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_import_permit_payment_mode_payment_mode_id",
                        column: x => x.payment_mode_id,
                        principalSchema: "common",
                        principalTable: "payment_mode",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_import_permit_port_of_entry_port_of_entry_id",
                        column: x => x.port_of_entry_id,
                        principalSchema: "common",
                        principalTable: "port_of_entry",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_import_permit_shipping_method_shipping_method_id",
                        column: x => x.shipping_method_id,
                        principalSchema: "common",
                        principalTable: "shipping_method",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "import_permit_detail",
                schema: "procurement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    amount = table.Column<decimal>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    import_permit_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    manufacturer_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    quantity = table.Column<decimal>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    unit_price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_import_permit_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_import_permit_detail_import_permit_import_permit_id",
                        column: x => x.import_permit_id,
                        principalSchema: "procurement",
                        principalTable: "import_permit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_import_permit_detail_manufacturer_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalSchema: "customer",
                        principalTable: "manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_import_permit_detail_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "commodity",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "import_permit_log_status",
                schema: "procurement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    comment = table.Column<string>(maxLength: 1000, nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    from_status_id = table.Column<int>(nullable: true),
                    import_permit_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    is_current = table.Column<bool>(nullable: false),
                    modified_by_user_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    to_status_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_import_permit_log_status", x => x.id);
                    table.ForeignKey(
                        name: "FK_import_permit_log_status_import_permit_status_from_status_id",
                        column: x => x.from_status_id,
                        principalSchema: "common",
                        principalTable: "import_permit_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_import_permit_log_status_import_permit_import_permit_id",
                        column: x => x.import_permit_id,
                        principalSchema: "procurement",
                        principalTable: "import_permit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_import_permit_log_status_user_modified_by_user_id",
                        column: x => x.modified_by_user_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_import_permit_log_status_import_permit_status_to_status_id",
                        column: x => x.to_status_id,
                        principalSchema: "common",
                        principalTable: "import_permit_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_agent_id",
                schema: "procurement",
                table: "import_permit",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_created_by_user_id",
                schema: "procurement",
                table: "import_permit",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_currency_id",
                schema: "procurement",
                table: "import_permit",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_import_permit_status_id",
                schema: "procurement",
                table: "import_permit",
                column: "import_permit_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_payment_mode_id",
                schema: "procurement",
                table: "import_permit",
                column: "payment_mode_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_port_of_entry_id",
                schema: "procurement",
                table: "import_permit",
                column: "port_of_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_shipping_method_id",
                schema: "procurement",
                table: "import_permit",
                column: "shipping_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_detail_import_permit_id",
                schema: "procurement",
                table: "import_permit_detail",
                column: "import_permit_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_detail_manufacturer_id",
                schema: "procurement",
                table: "import_permit_detail",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_detail_product_id",
                schema: "procurement",
                table: "import_permit_detail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_log_status_from_status_id",
                schema: "procurement",
                table: "import_permit_log_status",
                column: "from_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_log_status_import_permit_id",
                schema: "procurement",
                table: "import_permit_log_status",
                column: "import_permit_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_log_status_modified_by_user_id",
                schema: "procurement",
                table: "import_permit_log_status",
                column: "modified_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_log_status_to_status_id",
                schema: "procurement",
                table: "import_permit_log_status",
                column: "to_status_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "import_permit_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "import_permit_detail",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "import_permit_log_status",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "import_permit",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "import_permit_status",
                schema: "common");

            migrationBuilder.CreateTable(
                name: "purchase_order_status",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    purchase_order_status_code = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_order_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order_type",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    purchase_order_type_code = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_order_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order",
                schema: "procurement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_id = table.Column<int>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    created_by_user_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    currency_id = table.Column<int>(nullable: false),
                    delivery = table.Column<string>(maxLength: 100, nullable: false),
                    expiry_date = table.Column<DateTime>(nullable: true),
                    fee_received = table.Column<bool>(nullable: false),
                    fee_received_date = table.Column<DateTime>(nullable: true),
                    freight_cost = table.Column<decimal>(nullable: false),
                    gdt_id = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    payment_mode_id = table.Column<int>(nullable: false),
                    performa_invoice_number = table.Column<string>(nullable: true),
                    port_of_entry_id = table.Column<int>(nullable: false),
                    purchase_order_number = table.Column<string>(maxLength: 100, nullable: false),
                    purchase_order_status_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    shipping_method_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_purchase_order_agent_agent_id",
                        column: x => x.agent_id,
                        principalSchema: "customer",
                        principalTable: "agent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_user_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_currency_currency_id",
                        column: x => x.currency_id,
                        principalSchema: "common",
                        principalTable: "currency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_payment_mode_payment_mode_id",
                        column: x => x.payment_mode_id,
                        principalSchema: "common",
                        principalTable: "payment_mode",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_port_of_entry_port_of_entry_id",
                        column: x => x.port_of_entry_id,
                        principalSchema: "common",
                        principalTable: "port_of_entry",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_purchase_order_status_purchase_order_status_id",
                        column: x => x.purchase_order_status_id,
                        principalSchema: "common",
                        principalTable: "purchase_order_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_shipping_method_shipping_method_id",
                        column: x => x.shipping_method_id,
                        principalSchema: "common",
                        principalTable: "shipping_method",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order_detail",
                schema: "procurement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    amount = table.Column<decimal>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    manufacturer_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    purchase_order_id = table.Column<int>(nullable: false),
                    quantity = table.Column<decimal>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    unit_price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_order_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_purchase_order_detail_manufacturer_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalSchema: "customer",
                        principalTable: "manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_detail_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "commodity",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_detail_purchase_order_purchase_order_id",
                        column: x => x.purchase_order_id,
                        principalSchema: "procurement",
                        principalTable: "purchase_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order_log_status",
                schema: "procurement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    comment = table.Column<string>(maxLength: 1000, nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    from_status_id = table.Column<int>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    is_current = table.Column<bool>(nullable: false),
                    modified_by_user_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    purchase_order_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    to_status_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_order_log_status", x => x.id);
                    table.ForeignKey(
                        name: "FK_purchase_order_log_status_purchase_order_status_from_status_id",
                        column: x => x.from_status_id,
                        principalSchema: "common",
                        principalTable: "purchase_order_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_purchase_order_log_status_user_modified_by_user_id",
                        column: x => x.modified_by_user_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_log_status_purchase_order_purchase_order_id",
                        column: x => x.purchase_order_id,
                        principalSchema: "procurement",
                        principalTable: "purchase_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_log_status_purchase_order_status_to_status_id",
                        column: x => x.to_status_id,
                        principalSchema: "common",
                        principalTable: "purchase_order_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_agent_id",
                schema: "procurement",
                table: "purchase_order",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_created_by_user_id",
                schema: "procurement",
                table: "purchase_order",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_currency_id",
                schema: "procurement",
                table: "purchase_order",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_payment_mode_id",
                schema: "procurement",
                table: "purchase_order",
                column: "payment_mode_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_port_of_entry_id",
                schema: "procurement",
                table: "purchase_order",
                column: "port_of_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_purchase_order_status_id",
                schema: "procurement",
                table: "purchase_order",
                column: "purchase_order_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_shipping_method_id",
                schema: "procurement",
                table: "purchase_order",
                column: "shipping_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_detail_manufacturer_id",
                schema: "procurement",
                table: "purchase_order_detail",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_detail_product_id",
                schema: "procurement",
                table: "purchase_order_detail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_detail_purchase_order_id",
                schema: "procurement",
                table: "purchase_order_detail",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_log_status_from_status_id",
                schema: "procurement",
                table: "purchase_order_log_status",
                column: "from_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_log_status_modified_by_user_id",
                schema: "procurement",
                table: "purchase_order_log_status",
                column: "modified_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_log_status_purchase_order_id",
                schema: "procurement",
                table: "purchase_order_log_status",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_log_status_to_status_id",
                schema: "procurement",
                table: "purchase_order_log_status",
                column: "to_status_id");
        }
    }
}

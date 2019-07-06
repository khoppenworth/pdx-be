using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace pdx.dal.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.EnsureSchema(
                name: "commodity");

            migrationBuilder.EnsureSchema(
                name: "common");

            migrationBuilder.EnsureSchema(
                name: "customer");

            migrationBuilder.EnsureSchema(
                name: "document");

            migrationBuilder.EnsureSchema(
                name: "procurement");

            migrationBuilder.CreateTable(
                name: "menu",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    icon = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    menu_code = table.Column<string>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    parent_menu_id = table.Column<int>(nullable: true),
                    priority = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.id);
                    table.ForeignKey(
                        name: "FK_menu_menu_parent_menu_id",
                        column: x => x.parent_menu_id,
                        principalSchema: "account",
                        principalTable: "menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "permission",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    role_code = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "agent_type",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_type_code = table.Column<string>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agent_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "commodity_type",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    commodity_type_code = table.Column<string>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commodity_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    country_code = table.Column<string>(maxLength: 5, nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currency",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currency", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "document_type",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    direction = table.Column<bool>(nullable: false),
                    document_type_code = table.Column<string>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "module",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    module_code = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_mode",
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
                    payment_code = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_mode", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "port_of_entry",
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
                    port_code = table.Column<string>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_port_of_entry", x => x.id);
                });

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
                name: "shipping_method",
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
                    rowguid = table.Column<Guid>(nullable: false),
                    shipping_code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_method", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu_role",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    menu_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    role_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_role", x => x.id);
                    table.ForeignKey(
                        name: "FK_menu_role_menu_menu_id",
                        column: x => x.menu_id,
                        principalSchema: "account",
                        principalTable: "menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_menu_role_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "account",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    first_name = table.Column<string>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    last_login = table.Column<DateTime>(nullable: true),
                    last_name = table.Column<string>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    phone = table.Column<string>(nullable: true),
                    role_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    user_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "account",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    commodity_type_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    dosage_form = table.Column<string>(nullable: true),
                    dosage_strength = table.Column<string>(nullable: true),
                    dosage_unit = table.Column<string>(nullable: true),
                    generic_name = table.Column<string>(maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    shelf_life = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_commodity_type_commodity_type_id",
                        column: x => x.commodity_type_id,
                        principalSchema: "common",
                        principalTable: "commodity_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "address",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    city = table.Column<string>(maxLength: 100, nullable: true),
                    country_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    house_number = table.Column<string>(maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    kebele = table.Column<string>(maxLength: 100, nullable: true),
                    line1 = table.Column<string>(maxLength: 500, nullable: true),
                    line2 = table.Column<string>(maxLength: 500, nullable: true),
                    modified_date = table.Column<DateTime>(nullable: false),
                    place_name = table.Column<string>(maxLength: 100, nullable: true),
                    region = table.Column<string>(maxLength: 100, nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    street_name = table.Column<string>(maxLength: 100, nullable: true),
                    sub_city = table.Column<string>(maxLength: 100, nullable: true),
                    woreda = table.Column<string>(maxLength: 100, nullable: true),
                    zip_code = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                    table.ForeignKey(
                        name: "FK_address_country_country_id",
                        column: x => x.country_id,
                        principalSchema: "common",
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "manufacturer",
                schema: "customer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    country_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufacturer", x => x.id);
                    table.ForeignKey(
                        name: "FK_manufacturer_country_country_id",
                        column: x => x.country_id,
                        principalSchema: "common",
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permission",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    module_id = table.Column<int>(nullable: false),
                    permission_id = table.Column<int>(nullable: false),
                    role_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permission", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_permission_module_module_id",
                        column: x => x.module_id,
                        principalSchema: "common",
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_permission_permission_id",
                        column: x => x.permission_id,
                        principalSchema: "account",
                        principalTable: "permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "account",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "module_document",
                schema: "document",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    document_type_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    is_required = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    module_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module_document", x => x.id);
                    table.ForeignKey(
                        name: "FK_module_document_document_type_document_type_id",
                        column: x => x.document_type_id,
                        principalSchema: "common",
                        principalTable: "document_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_module_document_module_module_id",
                        column: x => x.module_id,
                        principalSchema: "common",
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agent",
                schema: "customer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    address_id = table.Column<int>(nullable: false),
                    agent_type_id = table.Column<int>(nullable: false),
                    contact_person = table.Column<string>(maxLength: 100, nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    email = table.Column<string>(maxLength: 100, nullable: true),
                    fax = table.Column<string>(maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    license_number = table.Column<string>(maxLength: 100, nullable: true),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    phone = table.Column<string>(maxLength: 100, nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    website = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agent", x => x.id);
                    table.ForeignKey(
                        name: "FK_agent_address_address_id",
                        column: x => x.address_id,
                        principalSchema: "common",
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_agent_agent_type_agent_type_id",
                        column: x => x.agent_type_id,
                        principalSchema: "common",
                        principalTable: "agent_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                schema: "customer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    address_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    email = table.Column<string>(maxLength: 100, nullable: true),
                    fax = table.Column<string>(maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    phone = table.Column<string>(maxLength: 100, nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    website = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier", x => x.id);
                    table.ForeignKey(
                        name: "FK_supplier_address_address_id",
                        column: x => x.address_id,
                        principalSchema: "common",
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "document",
                schema: "document",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_by = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    file_path = table.Column<string>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    module_document_id = table.Column<int>(nullable: false),
                    reference_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    updated_by = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document", x => x.id);
                    table.ForeignKey(
                        name: "FK_document_user_created_by",
                        column: x => x.created_by,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_document_module_document_module_document_id",
                        column: x => x.module_document_id,
                        principalSchema: "document",
                        principalTable: "module_document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_document_user_updated_by",
                        column: x => x.updated_by,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_agent",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_agent", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_agent_agent_agent_id",
                        column: x => x.agent_id,
                        principalSchema: "customer",
                        principalTable: "agent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_agent_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    CreatedByUserID = table.Column<int>(nullable: false),
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
                        name: "FK_purchase_order_user_CreatedByUserID",
                        column: x => x.CreatedByUserID,
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
                name: "IX_menu_parent_menu_id",
                schema: "account",
                table: "menu",
                column: "parent_menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_menu_role_menu_id",
                schema: "account",
                table: "menu_role",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_menu_role_role_id",
                schema: "account",
                table: "menu_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_module_id",
                schema: "account",
                table: "role_permission",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_permission_id",
                schema: "account",
                table: "role_permission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_role_id",
                schema: "account",
                table: "role_permission",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                schema: "account",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_agent_agent_id",
                schema: "account",
                table: "user_agent",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_agent_user_id",
                schema: "account",
                table: "user_agent",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_commodity_type_id",
                schema: "commodity",
                table: "product",
                column: "commodity_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_address_country_id",
                schema: "common",
                table: "address",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_agent_address_id",
                schema: "customer",
                table: "agent",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_agent_agent_type_id",
                schema: "customer",
                table: "agent",
                column: "agent_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_manufacturer_country_id",
                schema: "customer",
                table: "manufacturer",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_supplier_address_id",
                schema: "customer",
                table: "supplier",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_created_by",
                schema: "document",
                table: "document",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_document_module_document_id",
                schema: "document",
                table: "document",
                column: "module_document_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_updated_by",
                schema: "document",
                table: "document",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_module_document_document_type_id",
                schema: "document",
                table: "module_document",
                column: "document_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_module_document_module_id",
                schema: "document",
                table: "module_document",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_agent_id",
                schema: "procurement",
                table: "purchase_order",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_CreatedByUserID",
                schema: "procurement",
                table: "purchase_order",
                column: "CreatedByUserID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_role",
                schema: "account");

            migrationBuilder.DropTable(
                name: "role_permission",
                schema: "account");

            migrationBuilder.DropTable(
                name: "user_agent",
                schema: "account");

            migrationBuilder.DropTable(
                name: "purchase_order_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "supplier",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "document",
                schema: "document");

            migrationBuilder.DropTable(
                name: "purchase_order_detail",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "purchase_order_log_status",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "menu",
                schema: "account");

            migrationBuilder.DropTable(
                name: "permission",
                schema: "account");

            migrationBuilder.DropTable(
                name: "module_document",
                schema: "document");

            migrationBuilder.DropTable(
                name: "manufacturer",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "product",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "purchase_order",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "document_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "module",
                schema: "common");

            migrationBuilder.DropTable(
                name: "commodity_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "agent",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "user",
                schema: "account");

            migrationBuilder.DropTable(
                name: "currency",
                schema: "common");

            migrationBuilder.DropTable(
                name: "payment_mode",
                schema: "common");

            migrationBuilder.DropTable(
                name: "port_of_entry",
                schema: "common");

            migrationBuilder.DropTable(
                name: "purchase_order_status",
                schema: "common");

            migrationBuilder.DropTable(
                name: "shipping_method",
                schema: "common");

            migrationBuilder.DropTable(
                name: "address",
                schema: "common");

            migrationBuilder.DropTable(
                name: "agent_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "role",
                schema: "account");

            migrationBuilder.DropTable(
                name: "country",
                schema: "common");
        }
    }
}

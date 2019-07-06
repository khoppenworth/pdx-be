using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDX.DAL.Migrations
{
    public partial class Rebase20 : Migration
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
                name: "settings");

            migrationBuilder.EnsureSchema(
                name: "customer");

            migrationBuilder.EnsureSchema(
                name: "document");

            migrationBuilder.EnsureSchema(
                name: "logging");

            migrationBuilder.EnsureSchema(
                name: "procurement");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.EnsureSchema(
                name: "report");

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
                    category = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    permission_code = table.Column<string>(nullable: false),
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
                name: "user_login",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    browser = table.Column<string>(nullable: true),
                    browser_version = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    device_type = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    login_time = table.Column<DateTime>(nullable: false),
                    logout_reason = table.Column<string>(nullable: true),
                    logout_time = table.Column<DateTime>(nullable: true),
                    modified_date = table.Column<DateTime>(nullable: false),
                    os = table.Column<string>(nullable: true),
                    os_version = table.Column<string>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_login", x => x.id);
                });

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
                    rowguid = table.Column<Guid>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agent_level", x => x.id);
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
                    rowguid = table.Column<Guid>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
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
                    rowguid = table.Column<Guid>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
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
                    short_name = table.Column<string>(maxLength: 100, nullable: true),
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
                    document_type_code = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    is_system_generated = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "import_permit_delivery",
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
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_import_permit_delivery", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "import_permit_status",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    display_name = table.Column<string>(nullable: true),
                    import_permit_status_code = table.Column<string>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    priority = table.Column<int>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
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
                    rowguid = table.Column<Guid>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_import_permit_type", x => x.id);
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
                    rowguid = table.Column<Guid>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
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
                    rowguid = table.Column<Guid>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_mode", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_term",
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
                    payment_term_code = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_term", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report_type",
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
                    report_type_code = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_type", x => x.id);
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
                    shipping_code = table.Column<string>(nullable: true),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_method", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "system_setting",
                schema: "settings",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    data_type = table.Column<string>(maxLength: 100, nullable: true),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    system_setting_code = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_setting", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_type",
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
                    short_name = table.Column<string>(maxLength: 100, nullable: true),
                    user_type_code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_type", x => x.id);
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

            migrationBuilder.CreateTable(
                name: "change_log",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    content = table.Column<string>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    release_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    version = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_change_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vwagent",
                schema: "customer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    address_id = table.Column<int>(nullable: false),
                    agent_type_id = table.Column<int>(nullable: false),
                    agent_type_name = table.Column<string>(nullable: true),
                    contact_person = table.Column<string>(nullable: true),
                    country_name = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    fax = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    is_approved = table.Column<bool>(nullable: false),
                    license_number = table.Column<string>(nullable: true),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vwagent", x => x.id);
                });

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
                    assigned_date = table.Column<DateTime>(nullable: true),
                    assigned_user = table.Column<string>(nullable: true),
                    assigned_user_id = table.Column<int>(nullable: true),
                    created_by_user_id = table.Column<int>(nullable: false),
                    created_by_username = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    currency = table.Column<string>(nullable: true),
                    currency_id = table.Column<int>(nullable: false),
                    currency_sh = table.Column<string>(nullable: true),
                    currency_symbol = table.Column<string>(nullable: true),
                    decision_date = table.Column<DateTime>(nullable: true),
                    delivery = table.Column<string>(nullable: true),
                    discount = table.Column<decimal>(nullable: true),
                    expiry_date = table.Column<DateTime>(nullable: true),
                    freight_cost = table.Column<decimal>(nullable: false),
                    import_permit_number = table.Column<string>(nullable: true),
                    import_permit_status = table.Column<string>(nullable: true),
                    import_permit_status_code = table.Column<string>(nullable: true),
                    import_permit_status_display_name = table.Column<string>(nullable: true),
                    import_permit_status_id = table.Column<int>(nullable: false),
                    import_permit_status_priority = table.Column<int>(nullable: true),
                    import_permit_status_sh = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    payment_mode = table.Column<string>(nullable: true),
                    payment_mode_id = table.Column<int>(nullable: false),
                    payment_mode_sh = table.Column<string>(nullable: true),
                    performa_invoice_number = table.Column<string>(nullable: true),
                    port_of_entry = table.Column<string>(nullable: true),
                    port_of_entry_id = table.Column<int>(nullable: false),
                    port_of_entry_sh = table.Column<string>(nullable: true),
                    remark = table.Column<string>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    shipping_method = table.Column<string>(nullable: true),
                    shipping_method_id = table.Column<int>(nullable: false),
                    shipping_method_sh = table.Column<string>(nullable: true),
                    submission_date = table.Column<DateTime>(nullable: false),
                    supplier_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vwimport_permit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vwpip",
                schema: "procurement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_id = table.Column<int>(nullable: false),
                    agent_name = table.Column<string>(nullable: true),
                    amount = table.Column<decimal>(nullable: false),
                    assigned_date = table.Column<DateTime>(nullable: true),
                    assigned_user = table.Column<string>(nullable: true),
                    assigned_user_id = table.Column<int>(nullable: true),
                    created_by_user_id = table.Column<int>(nullable: false),
                    created_by_username = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    currency = table.Column<string>(nullable: true),
                    currency_id = table.Column<int>(nullable: false),
                    currency_sh = table.Column<string>(nullable: true),
                    currency_symbol = table.Column<string>(nullable: true),
                    decision_date = table.Column<DateTime>(nullable: true),
                    delivery = table.Column<string>(nullable: true),
                    discount = table.Column<decimal>(nullable: true),
                    expiry_date = table.Column<DateTime>(nullable: true),
                    freight_cost = table.Column<decimal>(nullable: false),
                    import_permit_number = table.Column<string>(nullable: true),
                    import_permit_status = table.Column<string>(nullable: true),
                    import_permit_status_code = table.Column<string>(nullable: true),
                    import_permit_status_display_name = table.Column<string>(nullable: true),
                    import_permit_status_id = table.Column<int>(nullable: false),
                    import_permit_status_priority = table.Column<int>(nullable: true),
                    import_permit_status_sh = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    payment_mode = table.Column<string>(nullable: true),
                    payment_mode_id = table.Column<int>(nullable: false),
                    payment_mode_sh = table.Column<string>(nullable: true),
                    performa_invoice_number = table.Column<string>(nullable: true),
                    port_of_entry = table.Column<string>(nullable: true),
                    port_of_entry_id = table.Column<int>(nullable: false),
                    port_of_entry_sh = table.Column<string>(nullable: true),
                    remark = table.Column<string>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    shipping_method = table.Column<string>(nullable: true),
                    shipping_method_id = table.Column<int>(nullable: false),
                    shipping_method_sh = table.Column<string>(nullable: true),
                    submission_date = table.Column<DateTime>(nullable: false),
                    supplier_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vwpip", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vwproduct",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_name = table.Column<string>(nullable: true),
                    brand_name = table.Column<string>(nullable: true),
                    commodity_type_name = table.Column<string>(nullable: true),
                    country_name = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    expiry_date = table.Column<DateTime>(nullable: true),
                    full_item_name = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    manufacturer_name = table.Column<string>(nullable: true),
                    modified_date = table.Column<DateTime>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    registration_date = table.Column<DateTime>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    shelf_life = table.Column<string>(nullable: true),
                    supplier_id = table.Column<int>(nullable: false),
                    supplier_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vwproduct", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vwsupplier",
                schema: "customer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    address_id = table.Column<int>(nullable: false),
                    agent_count = table.Column<int>(nullable: true),
                    country_name = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    fax = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    product_count = table.Column<int>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vwsupplier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vwuser",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_name = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    first_name = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    last_login = table.Column<DateTime>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    modified_date = table.Column<DateTime>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    user_type_code = table.Column<string>(nullable: true),
                    user_type_id = table.Column<int>(nullable: false),
                    user_type_name = table.Column<string>(nullable: true),
                    user_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vwuser", x => x.id);
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
                name: "role_permission",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    permission_id = table.Column<int>(nullable: false),
                    role_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permission", x => x.id);
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
                name: "product",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    brand_name = table.Column<string>(maxLength: 500, nullable: true),
                    commodity_type_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    dosage_form = table.Column<string>(nullable: true),
                    dosage_strength = table.Column<string>(nullable: true),
                    dosage_unit = table.Column<string>(nullable: true),
                    generic_name = table.Column<string>(maxLength: 500, nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 500, nullable: false),
                    presentation = table.Column<string>(maxLength: 500, nullable: true),
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
                    name = table.Column<string>(maxLength: 300, nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    site = table.Column<string>(nullable: true)
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
                name: "submodule",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    module_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true),
                    submodule_code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submodule", x => x.id);
                    table.ForeignKey(
                        name: "FK_submodule_module_module_id",
                        column: x => x.module_id,
                        principalSchema: "common",
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report",
                schema: "report",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    filter_columns = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    max_rows = table.Column<int>(nullable: true),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 500, nullable: false),
                    priority = table.Column<int>(nullable: true),
                    query = table.Column<string>(nullable: false),
                    report_type_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    series_columns = table.Column<string>(nullable: true),
                    title = table.Column<string>(maxLength: 500, nullable: false),
                    width = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report", x => x.id);
                    table.ForeignKey(
                        name: "FK_report_report_type_report_type_id",
                        column: x => x.report_type_id,
                        principalSchema: "common",
                        principalTable: "report_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    rowguid = table.Column<Guid>(nullable: false),
                    shipping_method_id = table.Column<int>(nullable: false),
                    short_name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_port_of_entry", x => x.id);
                    table.ForeignKey(
                        name: "FK_port_of_entry_shipping_method_shipping_method_id",
                        column: x => x.shipping_method_id,
                        principalSchema: "common",
                        principalTable: "shipping_method",
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
                    phone = table.Column<string>(maxLength: 100, nullable: true),
                    phone2 = table.Column<string>(maxLength: 100, nullable: true),
                    phone3 = table.Column<string>(maxLength: 100, nullable: true),
                    RoleID = table.Column<int>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    user_type_id = table.Column<int>(nullable: false),
                    user_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_role_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "account",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_user_type_user_type_id",
                        column: x => x.user_type_id,
                        principalSchema: "common",
                        principalTable: "user_type",
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
                    is_approved = table.Column<bool>(nullable: false),
                    license_number = table.Column<string>(maxLength: 100, nullable: true),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 500, nullable: false),
                    phone = table.Column<string>(maxLength: 100, nullable: true),
                    phone2 = table.Column<string>(maxLength: 100, nullable: true),
                    phone3 = table.Column<string>(maxLength: 100, nullable: true),
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
                    name = table.Column<string>(maxLength: 500, nullable: false),
                    phone = table.Column<string>(maxLength: 100, nullable: true),
                    phone2 = table.Column<string>(maxLength: 100, nullable: true),
                    phone3 = table.Column<string>(maxLength: 100, nullable: true),
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
                name: "product_manufacturer",
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
                    table.PrimaryKey("PK_product_manufacturer", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_manufacturer_manufacturer_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalSchema: "customer",
                        principalTable: "manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_manufacturer_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "commodity",
                        principalTable: "product",
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
                    rowguid = table.Column<Guid>(nullable: false),
                    submodule_id = table.Column<int>(nullable: false)
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
                        name: "FK_module_document_submodule_submodule_id",
                        column: x => x.submodule_id,
                        principalSchema: "common",
                        principalTable: "submodule",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report_role",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    report_id = table.Column<int>(nullable: false),
                    role_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_role", x => x.id);
                    table.ForeignKey(
                        name: "FK_report_role_report_report_id",
                        column: x => x.report_id,
                        principalSchema: "report",
                        principalTable: "report",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_report_role_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "account",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                schema: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    role_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_role_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "account",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "status_log",
                schema: "logging",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    column_name = table.Column<string>(nullable: true),
                    column_type = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    entity_id = table.Column<int>(nullable: false),
                    entity_type = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_by_user_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    new_value = table.Column<string>(nullable: true),
                    old_value = table.Column<string>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_status_log_user_modified_by_user_id",
                        column: x => x.modified_by_user_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wip",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    content = table.Column<string>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    type = table.Column<string>(maxLength: 100, nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wip", x => x.id);
                    table.ForeignKey(
                        name: "FK_wip_user_user_id",
                        column: x => x.user_id,
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
                name: "agent_product",
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
                    table.PrimaryKey("PK_agent_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_agent_product_agent_agent_id",
                        column: x => x.agent_id,
                        principalSchema: "customer",
                        principalTable: "agent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_agent_product_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "commodity",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "supplier_product",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    expiry_date = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    registration_date = table.Column<DateTime>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    supplier_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_supplier_product_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "commodity",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_supplier_product_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "customer",
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    end_date = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    remark = table.Column<string>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "import_permit",
                schema: "procurement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_id = table.Column<int>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    assigned_date = table.Column<DateTime>(nullable: true),
                    assigned_user_id = table.Column<int>(nullable: true),
                    created_by_user_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    currency_id = table.Column<int>(nullable: false),
                    delivery = table.Column<string>(maxLength: 100, nullable: false),
                    discount = table.Column<decimal>(nullable: true),
                    expiry_date = table.Column<DateTime>(nullable: true),
                    freight_cost = table.Column<decimal>(nullable: false),
                    import_permit_number = table.Column<string>(maxLength: 100, nullable: false),
                    import_permit_status_id = table.Column<int>(nullable: false),
                    import_permit_type_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    payment_mode_id = table.Column<int>(nullable: false),
                    payment_term_id = table.Column<int>(nullable: true),
                    performa_invoice_number = table.Column<string>(nullable: true),
                    port_of_entry_id = table.Column<int>(nullable: false),
                    remark = table.Column<string>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    shipping_method_id = table.Column<int>(nullable: false),
                    supplier_id = table.Column<int>(nullable: false)
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
                        name: "FK_import_permit_user_assigned_user_id",
                        column: x => x.assigned_user_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_import_permit_import_permit_type_import_permit_type_id",
                        column: x => x.import_permit_type_id,
                        principalSchema: "common",
                        principalTable: "import_permit_type",
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
                        name: "FK_import_permit_payment_term_payment_term_id",
                        column: x => x.payment_term_id,
                        principalSchema: "common",
                        principalTable: "payment_term",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_import_permit_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "customer",
                        principalTable: "supplier",
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
                    file_type = table.Column<string>(nullable: false),
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
                name: "import_permit_detail",
                schema: "procurement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    amount = table.Column<decimal>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    discount = table.Column<decimal>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "print_log",
                schema: "document",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    document_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    printed_by_user_id = table.Column<int>(nullable: false),
                    printed_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_print_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_print_log_document_document_id",
                        column: x => x.document_id,
                        principalSchema: "document",
                        principalTable: "document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_print_log_user_printed_by_user_id",
                        column: x => x.printed_by_user_id,
                        principalSchema: "account",
                        principalTable: "user",
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
                name: "IX_report_role_report_id",
                schema: "account",
                table: "report_role",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_role_role_id",
                schema: "account",
                table: "report_role",
                column: "role_id");

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
                name: "IX_user_RoleID",
                schema: "account",
                table: "user",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_user_user_type_id",
                schema: "account",
                table: "user",
                column: "user_type_id");

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
                name: "IX_user_role_role_id",
                schema: "account",
                table: "user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_user_id",
                schema: "account",
                table: "user_role",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_agent_product_agent_id",
                schema: "commodity",
                table: "agent_product",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_agent_product_product_id",
                schema: "commodity",
                table: "agent_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_commodity_type_id",
                schema: "commodity",
                table: "product",
                column: "commodity_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_manufacturer_manufacturer_id",
                schema: "commodity",
                table: "product_manufacturer",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_manufacturer_product_id",
                schema: "commodity",
                table: "product_manufacturer",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_supplier_product_product_id",
                schema: "commodity",
                table: "supplier_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_supplier_product_supplier_id",
                schema: "commodity",
                table: "supplier_product",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_address_country_id",
                schema: "common",
                table: "address",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_port_of_entry_shipping_method_id",
                schema: "common",
                table: "port_of_entry",
                column: "shipping_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_submodule_module_id",
                schema: "common",
                table: "submodule",
                column: "module_id");

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
                name: "IX_letter_module_document_id",
                schema: "document",
                table: "letter",
                column: "module_document_id");

            migrationBuilder.CreateIndex(
                name: "IX_module_document_document_type_id",
                schema: "document",
                table: "module_document",
                column: "document_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_module_document_submodule_id",
                schema: "document",
                table: "module_document",
                column: "submodule_id");

            migrationBuilder.CreateIndex(
                name: "IX_print_log_document_id",
                schema: "document",
                table: "print_log",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "IX_print_log_printed_by_user_id",
                schema: "document",
                table: "print_log",
                column: "printed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_status_log_modified_by_user_id",
                schema: "logging",
                table: "status_log",
                column: "modified_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_agent_id",
                schema: "procurement",
                table: "import_permit",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_assigned_user_id",
                schema: "procurement",
                table: "import_permit",
                column: "assigned_user_id");

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
                name: "IX_import_permit_import_permit_type_id",
                schema: "procurement",
                table: "import_permit",
                column: "import_permit_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_payment_mode_id",
                schema: "procurement",
                table: "import_permit",
                column: "payment_mode_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_permit_payment_term_id",
                schema: "procurement",
                table: "import_permit",
                column: "payment_term_id");

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
                name: "IX_import_permit_supplier_id",
                schema: "procurement",
                table: "import_permit",
                column: "supplier_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_wip_user_id",
                schema: "public",
                table: "wip",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_report_type_id",
                schema: "report",
                table: "report",
                column: "report_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_role",
                schema: "account");

            migrationBuilder.DropTable(
                name: "report_role",
                schema: "account");

            migrationBuilder.DropTable(
                name: "role_permission",
                schema: "account");

            migrationBuilder.DropTable(
                name: "user_agent",
                schema: "account");

            migrationBuilder.DropTable(
                name: "user_login",
                schema: "account");

            migrationBuilder.DropTable(
                name: "user_role",
                schema: "account");

            migrationBuilder.DropTable(
                name: "agent_product",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "product_manufacturer",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "supplier_product",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "import_permit_delivery",
                schema: "common");

            migrationBuilder.DropTable(
                name: "system_setting",
                schema: "settings");

            migrationBuilder.DropTable(
                name: "agent_supplier",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "letter",
                schema: "document");

            migrationBuilder.DropTable(
                name: "letter_heading",
                schema: "document");

            migrationBuilder.DropTable(
                name: "print_log",
                schema: "document");

            migrationBuilder.DropTable(
                name: "status_log",
                schema: "logging");

            migrationBuilder.DropTable(
                name: "import_permit_detail",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "import_permit_log_status",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "change_log",
                schema: "public");

            migrationBuilder.DropTable(
                name: "wip",
                schema: "public");

            migrationBuilder.DropTable(
                name: "vwagent",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "vwimport_permit",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "vwpip",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "vwproduct",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "vwsupplier",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "vwuser",
                schema: "account");

            migrationBuilder.DropTable(
                name: "menu",
                schema: "account");

            migrationBuilder.DropTable(
                name: "report",
                schema: "report");

            migrationBuilder.DropTable(
                name: "permission",
                schema: "account");

            migrationBuilder.DropTable(
                name: "agent_level",
                schema: "common");

            migrationBuilder.DropTable(
                name: "document",
                schema: "document");

            migrationBuilder.DropTable(
                name: "manufacturer",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "product",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "import_permit",
                schema: "procurement");

            migrationBuilder.DropTable(
                name: "report_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "module_document",
                schema: "document");

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
                name: "import_permit_status",
                schema: "common");

            migrationBuilder.DropTable(
                name: "import_permit_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "payment_mode",
                schema: "common");

            migrationBuilder.DropTable(
                name: "payment_term",
                schema: "common");

            migrationBuilder.DropTable(
                name: "port_of_entry",
                schema: "common");

            migrationBuilder.DropTable(
                name: "supplier",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "document_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "submodule",
                schema: "common");

            migrationBuilder.DropTable(
                name: "agent_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "role",
                schema: "account");

            migrationBuilder.DropTable(
                name: "user_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "shipping_method",
                schema: "common");

            migrationBuilder.DropTable(
                name: "address",
                schema: "common");

            migrationBuilder.DropTable(
                name: "module",
                schema: "common");

            migrationBuilder.DropTable(
                name: "country",
                schema: "common");
        }
    }
}

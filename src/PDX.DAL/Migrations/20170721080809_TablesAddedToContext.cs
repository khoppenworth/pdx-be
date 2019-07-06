using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDX.DAL.Migrations
{
    public partial class TablesAddedToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.EnsureSchema(
                name: "finance");

            migrationBuilder.EnsureSchema(
                name: "license");

            migrationBuilder.CreateTable(
                name: "atc",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    atc_code = table.Column<string>(maxLength: 10, nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atc", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "excipient",
                schema: "commodity",
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
                    table.PrimaryKey("PK_excipient", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "use_category",
                schema: "commodity",
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
                    table.PrimaryKey("PK_use_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "answer_type",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    answer_type_code = table.Column<string>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "checklist_type",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    checklist_type_code = table.Column<string>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checklist_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fee_type",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    fee_type_code = table.Column<string>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fee_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "foreign_application_status",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    foreign_application_status_code = table.Column<string>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foreign_application_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ma_status",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    display_name = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    ma_status_code = table.Column<string>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    priority = table.Column<int>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ma_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ma_type",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    ma_type_code = table.Column<string>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ma_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "option_group",
                schema: "common",
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
                    table.PrimaryKey("PK_option_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "responder_type",
                schema: "common",
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
                    table.PrimaryKey("PK_responder_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sra_type",
                schema: "common",
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
                    table.PrimaryKey("PK_sra_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_atc",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    atc_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_atc", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_atc_atc_atc_id",
                        column: x => x.atc_id,
                        principalSchema: "commodity",
                        principalTable: "atc",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_atc_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "commodity",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_composition",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    dosage_strength = table.Column<string>(nullable: true),
                    dosage_unit_id = table.Column<int>(nullable: false),
                    excipient_id = table.Column<int>(nullable: false),
                    inn_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    is_active_composition = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    pharmacopoeia_tandard_id = table.Column<int>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_composition", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_composition_dosage_unit_dosage_unit_id",
                        column: x => x.dosage_unit_id,
                        principalSchema: "commodity",
                        principalTable: "dosage_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_composition_excipient_excipient_id",
                        column: x => x.excipient_id,
                        principalSchema: "commodity",
                        principalTable: "excipient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_composition_inn_inn_id",
                        column: x => x.inn_id,
                        principalSchema: "commodity",
                        principalTable: "inn",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_composition_pharmacopoeia_standard_pharmacopoeia_tandard_id",
                        column: x => x.pharmacopoeia_tandard_id,
                        principalSchema: "commodity",
                        principalTable: "pharmacopoeia_standard",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_composition_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "commodity",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checklist",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    answer_type_id = table.Column<int>(nullable: false),
                    checklist_type_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    is_sra = table.Column<bool>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    parent_checklist_id = table.Column<int>(nullable: false),
                    priority = table.Column<int>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checklist", x => x.id);
                    table.ForeignKey(
                        name: "FK_checklist_answer_type_answer_type_id",
                        column: x => x.answer_type_id,
                        principalSchema: "common",
                        principalTable: "answer_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checklist_checklist_type_checklist_type_id",
                        column: x => x.checklist_type_id,
                        principalSchema: "common",
                        principalTable: "checklist_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "submodule_fee",
                schema: "finance",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    fee = table.Column<decimal>(nullable: false),
                    fee_type_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    submodule_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submodule_fee", x => x.id);
                    table.ForeignKey(
                        name: "FK_submodule_fee_fee_type_fee_type_id",
                        column: x => x.fee_type_id,
                        principalSchema: "common",
                        principalTable: "fee_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_submodule_fee_submodule_submodule_id",
                        column: x => x.submodule_id,
                        principalSchema: "common",
                        principalTable: "submodule",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ma",
                schema: "license",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    agent_id = table.Column<int>(nullable: false),
                    created_by_user_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    expiry_date = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    is_sra = table.Column<bool>(nullable: false),
                    ma_number = table.Column<string>(maxLength: 100, nullable: false),
                    ma_status_id = table.Column<int>(nullable: false),
                    ma_type_id = table.Column<int>(nullable: false),
                    modified_by_user_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    registration_date = table.Column<DateTime>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false),
                    supplier_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ma", x => x.id);
                    table.ForeignKey(
                        name: "FK_ma_agent_agent_id",
                        column: x => x.agent_id,
                        principalSchema: "customer",
                        principalTable: "agent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ma_user_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ma_ma_status_ma_status_id",
                        column: x => x.ma_status_id,
                        principalSchema: "common",
                        principalTable: "ma_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ma_ma_type_ma_type_id",
                        column: x => x.ma_type_id,
                        principalSchema: "common",
                        principalTable: "ma_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ma_user_modified_by_user_id",
                        column: x => x.modified_by_user_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ma_supplier_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "customer",
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "option",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    option_group_id = table.Column<int>(nullable: false),
                    priority = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_option", x => x.id);
                    table.ForeignKey(
                        name: "FK_option_option_group_option_group_id",
                        column: x => x.option_group_id,
                        principalSchema: "common",
                        principalTable: "option_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sra",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    sra_code = table.Column<string>(maxLength: 10, nullable: false),
                    sra_type_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sra", x => x.id);
                    table.ForeignKey(
                        name: "FK_sra_sra_type_sra_type_id",
                        column: x => x.sra_type_id,
                        principalSchema: "common",
                        principalTable: "sra_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "submodule_checklist",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    checklist_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    submodule_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submodule_checklist", x => x.id);
                    table.ForeignKey(
                        name: "FK_submodule_checklist_checklist_checklist_id",
                        column: x => x.checklist_id,
                        principalSchema: "catalog",
                        principalTable: "checklist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_submodule_checklist_submodule_submodule_id",
                        column: x => x.submodule_id,
                        principalSchema: "common",
                        principalTable: "submodule",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ma_payment",
                schema: "finance",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    ma_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    paid_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    submodule_fee_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ma_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_ma_payment_submodule_fee_submodule_fee_id",
                        column: x => x.submodule_fee_id,
                        principalSchema: "finance",
                        principalTable: "submodule_fee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "foreign_application",
                schema: "license",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    certificate_number = table.Column<string>(nullable: true),
                    country_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    decision_date = table.Column<DateTime>(nullable: true),
                    foreign_application_status_id = table.Column<int>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    ma_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foreign_application", x => x.id);
                    table.ForeignKey(
                        name: "FK_foreign_application_country_country_id",
                        column: x => x.country_id,
                        principalSchema: "common",
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_foreign_application_foreign_application_status_foreign_application_status_id",
                        column: x => x.foreign_application_status_id,
                        principalSchema: "common",
                        principalTable: "foreign_application_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_foreign_application_ma_ma_id",
                        column: x => x.ma_id,
                        principalSchema: "license",
                        principalTable: "ma",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ma_log_status",
                schema: "license",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    comment = table.Column<string>(maxLength: 1000, nullable: true),
                    created_date = table.Column<DateTime>(nullable: false),
                    from_status_id = table.Column<int>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    is_current = table.Column<bool>(nullable: false),
                    ma_id = table.Column<int>(nullable: false),
                    modified_by_user_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false),
                    to_status_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ma_log_status", x => x.id);
                    table.ForeignKey(
                        name: "FK_ma_log_status_ma_status_from_status_id",
                        column: x => x.from_status_id,
                        principalSchema: "common",
                        principalTable: "ma_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ma_log_status_ma_ma_id",
                        column: x => x.ma_id,
                        principalSchema: "license",
                        principalTable: "ma",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ma_log_status_user_modified_by_user_id",
                        column: x => x.modified_by_user_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ma_log_status_ma_status_to_status_id",
                        column: x => x.to_status_id,
                        principalSchema: "common",
                        principalTable: "ma_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ma_checklist",
                schema: "license",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    answer = table.Column<string>(nullable: true),
                    checklist_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    ma_id = table.Column<int>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    option_id = table.Column<int>(nullable: true),
                    responder_id = table.Column<int>(nullable: false),
                    responder_type_id = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ma_checklist", x => x.id);
                    table.ForeignKey(
                        name: "FK_ma_checklist_checklist_checklist_id",
                        column: x => x.checklist_id,
                        principalSchema: "catalog",
                        principalTable: "checklist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ma_checklist_ma_ma_id",
                        column: x => x.ma_id,
                        principalSchema: "license",
                        principalTable: "ma",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ma_checklist_option_option_id",
                        column: x => x.option_id,
                        principalSchema: "catalog",
                        principalTable: "option",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ma_checklist_user_responder_id",
                        column: x => x.responder_id,
                        principalSchema: "account",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ma_checklist_responder_type_responder_type_id",
                        column: x => x.responder_type_id,
                        principalSchema: "common",
                        principalTable: "responder_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checklist_answer_type_id",
                schema: "catalog",
                table: "checklist",
                column: "answer_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_checklist_checklist_type_id",
                schema: "catalog",
                table: "checklist",
                column: "checklist_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_option_option_group_id",
                schema: "catalog",
                table: "option",
                column: "option_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_submodule_checklist_checklist_id",
                schema: "catalog",
                table: "submodule_checklist",
                column: "checklist_id");

            migrationBuilder.CreateIndex(
                name: "IX_submodule_checklist_submodule_id",
                schema: "catalog",
                table: "submodule_checklist",
                column: "submodule_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_atc_atc_id",
                schema: "commodity",
                table: "product_atc",
                column: "atc_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_atc_product_id",
                schema: "commodity",
                table: "product_atc",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_composition_dosage_unit_id",
                schema: "commodity",
                table: "product_composition",
                column: "dosage_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_composition_excipient_id",
                schema: "commodity",
                table: "product_composition",
                column: "excipient_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_composition_inn_id",
                schema: "commodity",
                table: "product_composition",
                column: "inn_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_composition_pharmacopoeia_tandard_id",
                schema: "commodity",
                table: "product_composition",
                column: "pharmacopoeia_tandard_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_composition_product_id",
                schema: "commodity",
                table: "product_composition",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_sra_sra_type_id",
                schema: "commodity",
                table: "sra",
                column: "sra_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_payment_submodule_fee_id",
                schema: "finance",
                table: "ma_payment",
                column: "submodule_fee_id");

            migrationBuilder.CreateIndex(
                name: "IX_submodule_fee_fee_type_id",
                schema: "finance",
                table: "submodule_fee",
                column: "fee_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_submodule_fee_submodule_id",
                schema: "finance",
                table: "submodule_fee",
                column: "submodule_id");

            migrationBuilder.CreateIndex(
                name: "IX_foreign_application_country_id",
                schema: "license",
                table: "foreign_application",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_foreign_application_foreign_application_status_id",
                schema: "license",
                table: "foreign_application",
                column: "foreign_application_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_foreign_application_ma_id",
                schema: "license",
                table: "foreign_application",
                column: "ma_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_agent_id",
                schema: "license",
                table: "ma",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_created_by_user_id",
                schema: "license",
                table: "ma",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_ma_status_id",
                schema: "license",
                table: "ma",
                column: "ma_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_ma_type_id",
                schema: "license",
                table: "ma",
                column: "ma_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_modified_by_user_id",
                schema: "license",
                table: "ma",
                column: "modified_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_supplier_id",
                schema: "license",
                table: "ma",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_checklist_checklist_id",
                schema: "license",
                table: "ma_checklist",
                column: "checklist_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_checklist_ma_id",
                schema: "license",
                table: "ma_checklist",
                column: "ma_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_checklist_option_id",
                schema: "license",
                table: "ma_checklist",
                column: "option_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_checklist_responder_id",
                schema: "license",
                table: "ma_checklist",
                column: "responder_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_checklist_responder_type_id",
                schema: "license",
                table: "ma_checklist",
                column: "responder_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_log_status_from_status_id",
                schema: "license",
                table: "ma_log_status",
                column: "from_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_log_status_ma_id",
                schema: "license",
                table: "ma_log_status",
                column: "ma_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_log_status_modified_by_user_id",
                schema: "license",
                table: "ma_log_status",
                column: "modified_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ma_log_status_to_status_id",
                schema: "license",
                table: "ma_log_status",
                column: "to_status_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "submodule_checklist",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_atc",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "product_composition",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "sra",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "use_category",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "ma_payment",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "foreign_application",
                schema: "license");

            migrationBuilder.DropTable(
                name: "ma_checklist",
                schema: "license");

            migrationBuilder.DropTable(
                name: "ma_log_status",
                schema: "license");

            migrationBuilder.DropTable(
                name: "atc",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "excipient",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "sra_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "submodule_fee",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "foreign_application_status",
                schema: "common");

            migrationBuilder.DropTable(
                name: "checklist",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "option",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "responder_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "ma",
                schema: "license");

            migrationBuilder.DropTable(
                name: "fee_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "answer_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "checklist_type",
                schema: "common");

            migrationBuilder.DropTable(
                name: "option_group",
                schema: "common");

            migrationBuilder.DropTable(
                name: "ma_status",
                schema: "common");

            migrationBuilder.DropTable(
                name: "ma_type",
                schema: "common");
        }
    }
}

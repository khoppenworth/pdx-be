using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDX.DAL.Migrations
{
    public partial class MATableMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "address_id",
                schema: "customer",
                table: "manufacturer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "customer",
                table: "manufacturer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fax",
                schema: "customer",
                table: "manufacturer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gmp_certificate_number",
                schema: "customer",
                table: "manufacturer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "gmp_inspected_date",
                schema: "customer",
                table: "manufacturer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_gmp_inspected",
                schema: "customer",
                table: "manufacturer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone",
                schema: "customer",
                table: "manufacturer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone2",
                schema: "customer",
                table: "manufacturer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone3",
                schema: "customer",
                table: "manufacturer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "website",
                schema: "customer",
                table: "manufacturer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "manufacturer_type_id",
                schema: "commodity",
                table: "product_manufacturer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "admin_route_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "age_group_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "container_type_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "created_by_user_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dosage_form_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dosage_unit_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "inn_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ingredient_statement",
                schema: "commodity",
                table: "product",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "modified_by_user_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pharmacological_classification_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pharmacopoei_standard_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "posology",
                schema: "commodity",
                table: "product",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "product_category_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "product_type_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "shelf_life_id",
                schema: "commodity",
                table: "product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "storage_condition",
                schema: "commodity",
                table: "product",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "admin_route",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    admin_route_code = table.Column<string>(maxLength: 10, nullable: false),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_route", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "age_group",
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
                    table.PrimaryKey("PK_age_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "container_type",
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
                    table.PrimaryKey("PK_container_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dosage_form",
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
                    table.PrimaryKey("PK_dosage_form", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dosage_unit",
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
                    table.PrimaryKey("PK_dosage_unit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inn",
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
                    table.PrimaryKey("PK_inn", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pharmacological_classification",
                schema: "commodity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    prefix = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pharmacological_classification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pharmacopoeia_standard",
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
                    table.PrimaryKey("PK_pharmacopoeia_standard", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_category",
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
                    table.PrimaryKey("PK_product_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_type",
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
                    table.PrimaryKey("PK_product_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shelf_life",
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
                    table.PrimaryKey("PK_shelf_life", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "manufacturer_type",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_date = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    manufacturer_type_code = table.Column<string>(nullable: false),
                    modified_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufacturer_type", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_manufacturer_address_id",
                schema: "customer",
                table: "manufacturer",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_manufacturer_manufacturer_type_id",
                schema: "commodity",
                table: "product_manufacturer",
                column: "manufacturer_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_admin_route_id",
                schema: "commodity",
                table: "product",
                column: "admin_route_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_age_group_id",
                schema: "commodity",
                table: "product",
                column: "age_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_container_type_id",
                schema: "commodity",
                table: "product",
                column: "container_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_created_by_user_id",
                schema: "commodity",
                table: "product",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_dosage_form_id",
                schema: "commodity",
                table: "product",
                column: "dosage_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_dosage_unit_id",
                schema: "commodity",
                table: "product",
                column: "dosage_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_inn_id",
                schema: "commodity",
                table: "product",
                column: "inn_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_modified_by_user_id",
                schema: "commodity",
                table: "product",
                column: "modified_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_pharmacological_classification_id",
                schema: "commodity",
                table: "product",
                column: "pharmacological_classification_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_pharmacopoei_standard_id",
                schema: "commodity",
                table: "product",
                column: "pharmacopoei_standard_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_product_category_id",
                schema: "commodity",
                table: "product",
                column: "product_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_product_type_id",
                schema: "commodity",
                table: "product",
                column: "product_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_shelf_life_id",
                schema: "commodity",
                table: "product",
                column: "shelf_life_id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_admin_route_admin_route_id",
                schema: "commodity",
                table: "product",
                column: "admin_route_id",
                principalSchema: "commodity",
                principalTable: "admin_route",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_age_group_age_group_id",
                schema: "commodity",
                table: "product",
                column: "age_group_id",
                principalSchema: "commodity",
                principalTable: "age_group",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_container_type_container_type_id",
                schema: "commodity",
                table: "product",
                column: "container_type_id",
                principalSchema: "commodity",
                principalTable: "container_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_user_created_by_user_id",
                schema: "commodity",
                table: "product",
                column: "created_by_user_id",
                principalSchema: "account",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_dosage_form_dosage_form_id",
                schema: "commodity",
                table: "product",
                column: "dosage_form_id",
                principalSchema: "commodity",
                principalTable: "dosage_form",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_dosage_unit_dosage_unit_id",
                schema: "commodity",
                table: "product",
                column: "dosage_unit_id",
                principalSchema: "commodity",
                principalTable: "dosage_unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_inn_inn_id",
                schema: "commodity",
                table: "product",
                column: "inn_id",
                principalSchema: "commodity",
                principalTable: "inn",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_user_modified_by_user_id",
                schema: "commodity",
                table: "product",
                column: "modified_by_user_id",
                principalSchema: "account",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_pharmacological_classification_pharmacological_classification_id",
                schema: "commodity",
                table: "product",
                column: "pharmacological_classification_id",
                principalSchema: "commodity",
                principalTable: "pharmacological_classification",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_pharmacopoeia_standard_pharmacopoei_standard_id",
                schema: "commodity",
                table: "product",
                column: "pharmacopoei_standard_id",
                principalSchema: "commodity",
                principalTable: "pharmacopoeia_standard",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_product_category_product_category_id",
                schema: "commodity",
                table: "product",
                column: "product_category_id",
                principalSchema: "commodity",
                principalTable: "product_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_product_type_product_type_id",
                schema: "commodity",
                table: "product",
                column: "product_type_id",
                principalSchema: "commodity",
                principalTable: "product_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_shelf_life_shelf_life_id",
                schema: "commodity",
                table: "product",
                column: "shelf_life_id",
                principalSchema: "commodity",
                principalTable: "shelf_life",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_manufacturer_manufacturer_type_manufacturer_type_id",
                schema: "commodity",
                table: "product_manufacturer",
                column: "manufacturer_type_id",
                principalSchema: "common",
                principalTable: "manufacturer_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_manufacturer_address_address_id",
                schema: "customer",
                table: "manufacturer",
                column: "address_id",
                principalSchema: "common",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_admin_route_admin_route_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_age_group_age_group_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_container_type_container_type_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_user_created_by_user_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_dosage_form_dosage_form_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_dosage_unit_dosage_unit_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_inn_inn_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_user_modified_by_user_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_pharmacological_classification_pharmacological_classification_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_pharmacopoeia_standard_pharmacopoei_standard_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_product_category_product_category_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_product_type_product_type_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_shelf_life_shelf_life_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_manufacturer_manufacturer_type_manufacturer_type_id",
                schema: "commodity",
                table: "product_manufacturer");

            migrationBuilder.DropForeignKey(
                name: "FK_manufacturer_address_address_id",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropTable(
                name: "admin_route",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "age_group",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "container_type",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "dosage_form",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "dosage_unit",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "inn",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "pharmacological_classification",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "pharmacopoeia_standard",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "product_category",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "product_type",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "shelf_life",
                schema: "commodity");

            migrationBuilder.DropTable(
                name: "manufacturer_type",
                schema: "common");

            migrationBuilder.DropIndex(
                name: "IX_manufacturer_address_id",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropIndex(
                name: "IX_product_manufacturer_manufacturer_type_id",
                schema: "commodity",
                table: "product_manufacturer");

            migrationBuilder.DropIndex(
                name: "IX_product_admin_route_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_age_group_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_container_type_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_created_by_user_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_dosage_form_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_dosage_unit_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_inn_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_modified_by_user_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_pharmacological_classification_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_pharmacopoei_standard_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_product_category_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_product_type_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_shelf_life_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "address_id",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropColumn(
                name: "email",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropColumn(
                name: "fax",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropColumn(
                name: "gmp_certificate_number",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropColumn(
                name: "gmp_inspected_date",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropColumn(
                name: "is_gmp_inspected",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropColumn(
                name: "phone",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropColumn(
                name: "phone2",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropColumn(
                name: "phone3",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropColumn(
                name: "website",
                schema: "customer",
                table: "manufacturer");

            migrationBuilder.DropColumn(
                name: "manufacturer_type_id",
                schema: "commodity",
                table: "product_manufacturer");

            migrationBuilder.DropColumn(
                name: "admin_route_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "age_group_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "container_type_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "created_by_user_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "dosage_form_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "dosage_unit_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "inn_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "ingredient_statement",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "modified_by_user_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "pharmacological_classification_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "pharmacopoei_standard_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "posology",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "product_category_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "product_type_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "shelf_life_id",
                schema: "commodity",
                table: "product");

            migrationBuilder.DropColumn(
                name: "storage_condition",
                schema: "commodity",
                table: "product");
        }
    }
}

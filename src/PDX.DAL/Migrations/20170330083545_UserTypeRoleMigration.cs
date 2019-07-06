using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PDX.DAL.Migrations
{
    public partial class UserTypeRoleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_role_role_id",
                schema: "account",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "role_id",
                schema: "account",
                table: "user",
                newName: "RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_id",
                schema: "account",
                table: "user",
                newName: "IX_user_RoleID");

            migrationBuilder.AlterColumn<int>(
                name: "RoleID",
                schema: "account",
                table: "user",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "user_type_id",
                schema: "account",
                table: "user",
                nullable: false,
                defaultValue: 0);

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
                    user_type_code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_type", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_user_type_id",
                schema: "account",
                table: "user",
                column: "user_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_RoleID",
                schema: "account",
                table: "user",
                column: "RoleID",
                principalSchema: "account",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_user_type_user_type_id",
                schema: "account",
                table: "user",
                column: "user_type_id",
                principalSchema: "common",
                principalTable: "user_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_role_RoleID",
                schema: "account",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_user_type_user_type_id",
                schema: "account",
                table: "user");

            migrationBuilder.DropTable(
                name: "user_type",
                schema: "common");

            migrationBuilder.DropIndex(
                name: "IX_user_user_type_id",
                schema: "account",
                table: "user");

            migrationBuilder.DropColumn(
                name: "user_type_id",
                schema: "account",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                schema: "account",
                table: "user",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_RoleID",
                schema: "account",
                table: "user",
                newName: "IX_user_role_id");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                schema: "account",
                table: "user",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_role_id",
                schema: "account",
                table: "user",
                column: "role_id",
                principalSchema: "account",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PDX.DAL.Migrations
{
    public partial class CurrencyInFeeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fee_type_code",
                schema: "common",
                table: "fee_type");

            migrationBuilder.AddColumn<int>(
                name: "currency_id",
                schema: "common",
                table: "fee_type",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_fee_type_currency_id",
                schema: "common",
                table: "fee_type",
                column: "currency_id");

            migrationBuilder.AddForeignKey(
                name: "FK_fee_type_currency_currency_id",
                schema: "common",
                table: "fee_type",
                column: "currency_id",
                principalSchema: "common",
                principalTable: "currency",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fee_type_currency_currency_id",
                schema: "common",
                table: "fee_type");

            migrationBuilder.DropIndex(
                name: "IX_fee_type_currency_id",
                schema: "common",
                table: "fee_type");

            migrationBuilder.DropColumn(
                name: "currency_id",
                schema: "common",
                table: "fee_type");

            migrationBuilder.AddColumn<string>(
                name: "fee_type_code",
                schema: "common",
                table: "fee_type",
                nullable: false,
                defaultValue: "");
        }
    }
}

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ćwiczenie4_KamilWolak.Migrations
{
    /// <inheritdoc />
    public partial class ChangetypeofEffectiveDatepropinExchangeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_ExchangeTables_ExchangeTableId",
                table: "Rates");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExchangeTableId",
                table: "Rates",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_ExchangeTables_ExchangeTableId",
                table: "Rates",
                column: "ExchangeTableId",
                principalTable: "ExchangeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_ExchangeTables_ExchangeTableId",
                table: "Rates");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExchangeTableId",
                table: "Rates",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_ExchangeTables_ExchangeTableId",
                table: "Rates",
                column: "ExchangeTableId",
                principalTable: "ExchangeTables",
                principalColumn: "Id");
        }
    }
}

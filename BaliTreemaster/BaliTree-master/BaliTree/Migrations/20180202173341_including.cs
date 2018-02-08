using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BaliTree.Migrations
{
    public partial class including : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_StockOrders_StockOrderId",
                table: "StockItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_StockTypes_TypeId",
                table: "StockItems");

            migrationBuilder.DropTable(
                name: "StockOrders");

            migrationBuilder.DropIndex(
                name: "IX_StockItems_StockOrderId",
                table: "StockItems");

            migrationBuilder.DropIndex(
                name: "IX_StockItems_TypeId",
                table: "StockItems");

            migrationBuilder.DropIndex(
                name: "IX_StockEvents_StockItemId",
                table: "StockEvents");

            migrationBuilder.DropColumn(
                name: "StockOrderId",
                table: "StockItems");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "StockItems");

            migrationBuilder.AlterColumn<int>(
                name: "InStock",
                table: "StockTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ItemTypeId",
                table: "StockItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_ItemTypeId",
                table: "StockItems",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockEvents_StockItemId",
                table: "StockEvents",
                column: "StockItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_StockTypes_ItemTypeId",
                table: "StockItems",
                column: "ItemTypeId",
                principalTable: "StockTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_StockTypes_ItemTypeId",
                table: "StockItems");

            migrationBuilder.DropIndex(
                name: "IX_StockItems_ItemTypeId",
                table: "StockItems");

            migrationBuilder.DropIndex(
                name: "IX_StockEvents_StockItemId",
                table: "StockEvents");

            migrationBuilder.DropColumn(
                name: "ItemTypeId",
                table: "StockItems");

            migrationBuilder.AlterColumn<int>(
                name: "InStock",
                table: "StockTypes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockOrderId",
                table: "StockItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "StockItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StockOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_StockOrderId",
                table: "StockItems",
                column: "StockOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_TypeId",
                table: "StockItems",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockEvents_StockItemId",
                table: "StockEvents",
                column: "StockItemId",
                unique: true,
                filter: "[StockItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_StockOrders_StockOrderId",
                table: "StockItems",
                column: "StockOrderId",
                principalTable: "StockOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_StockTypes_TypeId",
                table: "StockItems",
                column: "TypeId",
                principalTable: "StockTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

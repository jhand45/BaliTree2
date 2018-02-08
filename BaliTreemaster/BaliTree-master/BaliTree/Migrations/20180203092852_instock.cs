using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BaliTree.Migrations
{
    public partial class instock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountRecieved",
                table: "StockItems");

            migrationBuilder.AddColumn<int>(
                name: "InStock",
                table: "StockItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InStock",
                table: "StockItems");

            migrationBuilder.AddColumn<int>(
                name: "AmountRecieved",
                table: "StockItems",
                nullable: false,
                defaultValue: 0);
        }
    }
}

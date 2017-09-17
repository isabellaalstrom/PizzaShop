using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Migrations
{
    public partial class Changed_CartId_To_Guid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CartId", table: "CartItems");
            migrationBuilder.AddColumn<Guid>(
                name: "CartId",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "CartItems",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}

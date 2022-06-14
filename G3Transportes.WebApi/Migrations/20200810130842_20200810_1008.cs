using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G3Transportes.WebApi.Migrations
{
    public partial class _20200810_1008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataDevolucao",
                table: "Pedido",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Devolvido",
                table: "Pedido",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDevolucao",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "Devolvido",
                table: "Pedido");
        }
    }
}

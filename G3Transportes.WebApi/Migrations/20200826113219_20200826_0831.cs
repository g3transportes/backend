using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G3Transportes.WebApi.Migrations
{
    public partial class _20200826_0831 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Autorizado",
                table: "Lancamento",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAutorizacao",
                table: "Lancamento",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAutorizacao",
                table: "Lancamento",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autorizado",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "DataAutorizacao",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "UsuarioAutorizacao",
                table: "Lancamento");
        }
    }
}

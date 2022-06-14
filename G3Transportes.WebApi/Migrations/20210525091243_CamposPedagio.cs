using Microsoft.EntityFrameworkCore.Migrations;

namespace G3Transportes.WebApi.Migrations
{
    public partial class CamposPedagio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Administrador",
                table: "Usuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Financeiro",
                table: "Usuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "ValorPedagioCliente",
                table: "Pedido",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorPegadioG3",
                table: "Pedido",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorFreteG3",
                table: "Cliente",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorFreteTonelada",
                table: "Cliente",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Administrador",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Financeiro",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "ValorPedagioCliente",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "ValorPegadioG3",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "ValorFreteG3",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "ValorFreteTonelada",
                table: "Cliente");
        }
    }
}

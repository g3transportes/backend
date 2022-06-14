using Microsoft.EntityFrameworkCore.Migrations;

namespace G3Transportes.WebApi.Migrations
{
    public partial class _20200405_0025 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ValorUnitario",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "ValorLiquido",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "ValorFrete",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "ValorComissao",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "ValorBruto",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "Quantidade",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddColumn<double>(
                name: "ComissaoMargem",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ComissaoUnitario",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FreteUnitario",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "NumPedido",
                table: "Pedido",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ValorLiquido",
                table: "Lancamento",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "ValorDesconto",
                table: "Lancamento",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "ValorBruto",
                table: "Lancamento",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "ValorBaixado",
                table: "Lancamento",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "ValorAcrescimo",
                table: "Lancamento",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "Capacidade",
                table: "Caminhao",
                type: "double(12,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComissaoMargem",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "ComissaoUnitario",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "FreteUnitario",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "NumPedido",
                table: "Pedido");

            migrationBuilder.AlterColumn<double>(
                name: "ValorUnitario",
                table: "Pedido",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ValorLiquido",
                table: "Pedido",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ValorFrete",
                table: "Pedido",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ValorComissao",
                table: "Pedido",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ValorBruto",
                table: "Pedido",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Quantidade",
                table: "Pedido",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ValorLiquido",
                table: "Lancamento",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ValorDesconto",
                table: "Lancamento",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ValorBruto",
                table: "Lancamento",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ValorBaixado",
                table: "Lancamento",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ValorAcrescimo",
                table: "Lancamento",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Capacidade",
                table: "Caminhao",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(12,2)");
        }
    }
}

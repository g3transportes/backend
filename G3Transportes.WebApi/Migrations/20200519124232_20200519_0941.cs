using Microsoft.EntityFrameworkCore.Migrations;

namespace G3Transportes.WebApi.Migrations
{
    public partial class _20200519_0941 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdContaBancaria",
                table: "LancamentoBaixa",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdFormaPagamento",
                table: "LancamentoBaixa",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "LancamentoBaixa",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Creditos",
                table: "ContaBancaria",
                type: "double(12,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Debitos",
                table: "ContaBancaria",
                type: "double(12,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SaldoAtual",
                table: "ContaBancaria",
                type: "double(12,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SaldoInicial",
                table: "ContaBancaria",
                type: "double(12,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Padrao",
                table: "CentroCusto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "CentroCusto",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Renavam2",
                table: "Caminhao",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Renavam3",
                table: "Caminhao",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Renavam4",
                table: "Caminhao",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LancamentoBaixa_IdContaBancaria",
                table: "LancamentoBaixa",
                column: "IdContaBancaria");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentoBaixa_IdFormaPagamento",
                table: "LancamentoBaixa",
                column: "IdFormaPagamento");

            migrationBuilder.AddForeignKey(
                name: "FK_LancamentoBaixa_ContaBancaria_IdContaBancaria",
                table: "LancamentoBaixa",
                column: "IdContaBancaria",
                principalTable: "ContaBancaria",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LancamentoBaixa_FormaPagamento_IdFormaPagamento",
                table: "LancamentoBaixa",
                column: "IdFormaPagamento",
                principalTable: "FormaPagamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LancamentoBaixa_ContaBancaria_IdContaBancaria",
                table: "LancamentoBaixa");

            migrationBuilder.DropForeignKey(
                name: "FK_LancamentoBaixa_FormaPagamento_IdFormaPagamento",
                table: "LancamentoBaixa");

            migrationBuilder.DropIndex(
                name: "IX_LancamentoBaixa_IdContaBancaria",
                table: "LancamentoBaixa");

            migrationBuilder.DropIndex(
                name: "IX_LancamentoBaixa_IdFormaPagamento",
                table: "LancamentoBaixa");

            migrationBuilder.DropColumn(
                name: "IdContaBancaria",
                table: "LancamentoBaixa");

            migrationBuilder.DropColumn(
                name: "IdFormaPagamento",
                table: "LancamentoBaixa");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "LancamentoBaixa");

            migrationBuilder.DropColumn(
                name: "Creditos",
                table: "ContaBancaria");

            migrationBuilder.DropColumn(
                name: "Debitos",
                table: "ContaBancaria");

            migrationBuilder.DropColumn(
                name: "SaldoAtual",
                table: "ContaBancaria");

            migrationBuilder.DropColumn(
                name: "SaldoInicial",
                table: "ContaBancaria");

            migrationBuilder.DropColumn(
                name: "Padrao",
                table: "CentroCusto");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "CentroCusto");

            migrationBuilder.DropColumn(
                name: "Renavam2",
                table: "Caminhao");

            migrationBuilder.DropColumn(
                name: "Renavam3",
                table: "Caminhao");

            migrationBuilder.DropColumn(
                name: "Renavam4",
                table: "Caminhao");
        }
    }
}

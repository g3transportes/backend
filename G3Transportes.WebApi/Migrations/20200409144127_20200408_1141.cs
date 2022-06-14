using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G3Transportes.WebApi.Migrations
{
    public partial class _20200408_1141 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdCaminhao",
                table: "Pedido",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Coletado",
                table: "Pedido",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEntrega",
                table: "Pedido",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPagamento",
                table: "Pedido",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Entregue",
                table: "Pedido",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Pago",
                table: "Pedido",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "ValorAcrescimo",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorDesconto",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorPedagio",
                table: "Pedido",
                type: "double(12,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorSaldo",
                table: "Lancamento",
                type: "double(12,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "LancamentoAnexo",
                columns: table => new
                {
                    IdLancamento = table.Column<int>(nullable: false),
                    IdAnexo = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancamentoAnexo", x => new { x.IdLancamento, x.IdAnexo });
                    table.ForeignKey(
                        name: "FK_LancamentoAnexo_Anexo_IdAnexo",
                        column: x => x.IdAnexo,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LancamentoAnexo_Lancamento_IdLancamento",
                        column: x => x.IdLancamento,
                        principalTable: "Lancamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LancamentoBaixa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdLancamento = table.Column<int>(nullable: false),
                    Valor = table.Column<double>(type: "double(12,2)", nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancamentoBaixa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LancamentoBaixa_Lancamento_IdLancamento",
                        column: x => x.IdLancamento,
                        principalTable: "Lancamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LancamentoAnexo_IdAnexo",
                table: "LancamentoAnexo",
                column: "IdAnexo");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentoBaixa_IdLancamento",
                table: "LancamentoBaixa",
                column: "IdLancamento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LancamentoAnexo");

            migrationBuilder.DropTable(
                name: "LancamentoBaixa");

            migrationBuilder.DropColumn(
                name: "Coletado",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "DataEntrega",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "Entregue",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "Pago",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "ValorAcrescimo",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "ValorDesconto",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "ValorPedagio",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "ValorSaldo",
                table: "Lancamento");

            migrationBuilder.AlterColumn<int>(
                name: "IdCaminhao",
                table: "Pedido",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

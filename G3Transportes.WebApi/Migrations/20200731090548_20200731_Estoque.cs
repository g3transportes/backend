using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G3Transportes.WebApi.Migrations
{
    public partial class _20200731_Estoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstoqueAtual",
                table: "Remetente",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Paletes",
                table: "Pedido",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RemetenteEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdRemetente = table.Column<int>(nullable: false),
                    IdPedido = table.Column<int>(nullable: true),
                    Tipo = table.Column<string>(maxLength: 5, nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    Usuario = table.Column<string>(maxLength: 250, nullable: true),
                    Transferencia = table.Column<bool>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemetenteEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemetenteEstoque_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RemetenteEstoque_Remetente_IdRemetente",
                        column: x => x.IdRemetente,
                        principalTable: "Remetente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RemetenteEstoque_IdPedido",
                table: "RemetenteEstoque",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_RemetenteEstoque_IdRemetente",
                table: "RemetenteEstoque",
                column: "IdRemetente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemetenteEstoque");

            migrationBuilder.DropColumn(
                name: "EstoqueAtual",
                table: "Remetente");

            migrationBuilder.DropColumn(
                name: "Paletes",
                table: "Pedido");
        }
    }
}

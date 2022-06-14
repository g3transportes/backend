using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G3Transportes.WebApi.Migrations
{
    public partial class _20200513_0551 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CTe",
                table: "Pedido",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdRemetente",
                table: "Pedido",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NFe",
                table: "Pedido",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoAgencia",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoConta",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoDocumento",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoNome",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoOperacao",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoTitular",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHabilitacao",
                table: "Motorista",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVencimento",
                table: "Motorista",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndBairro",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndCep",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndCidade",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndComplemento",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndEstado",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndNumero",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndRua",
                table: "Motorista",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Motorista",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoAgencia",
                table: "Lancamento",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoConta",
                table: "Lancamento",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoDocumento",
                table: "Lancamento",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoNome",
                table: "Lancamento",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoOperacao",
                table: "Lancamento",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoTitular",
                table: "Lancamento",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCentroCusto",
                table: "Lancamento",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdContaBancaria",
                table: "Lancamento",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTipoDocumento",
                table: "Lancamento",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoAgencia",
                table: "Cliente",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoConta",
                table: "Cliente",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoDocumento",
                table: "Cliente",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoNome",
                table: "Cliente",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoOperacao",
                table: "Cliente",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BancoTitular",
                table: "Cliente",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Caminhao",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Caminhao",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProprietario",
                table: "Caminhao",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Placa2",
                table: "Caminhao",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Placa3",
                table: "Caminhao",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Placa4",
                table: "Caminhao",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Renavam",
                table: "Caminhao",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CentroCusto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Referencia = table.Column<string>(maxLength: 250, nullable: true),
                    Nome = table.Column<string>(maxLength: 250, nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroCusto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContaBancaria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 250, nullable: true),
                    Banco = table.Column<string>(maxLength: 250, nullable: true),
                    Agencia = table.Column<string>(maxLength: 250, nullable: true),
                    Operacao = table.Column<string>(maxLength: 250, nullable: true),
                    Conta = table.Column<string>(maxLength: 250, nullable: true),
                    Titular = table.Column<string>(maxLength: 250, nullable: true),
                    Documento = table.Column<string>(maxLength: 250, nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaBancaria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proprietario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 250, nullable: true),
                    Documento = table.Column<string>(maxLength: 250, nullable: true),
                    Documento2 = table.Column<string>(maxLength: 250, nullable: true),
                    Antt = table.Column<string>(maxLength: 250, nullable: true),
                    AnttData = table.Column<DateTime>(nullable: true),
                    Tipo = table.Column<string>(maxLength: 250, nullable: true),
                    Pis = table.Column<string>(maxLength: 250, nullable: true),
                    Filiacao = table.Column<string>(maxLength: 250, nullable: true),
                    Telefone1 = table.Column<string>(maxLength: 250, nullable: true),
                    Telefone2 = table.Column<string>(maxLength: 250, nullable: true),
                    EndRua = table.Column<string>(maxLength: 250, nullable: true),
                    EndNumero = table.Column<string>(maxLength: 250, nullable: true),
                    EndComplemento = table.Column<string>(maxLength: 250, nullable: true),
                    EndBairro = table.Column<string>(maxLength: 250, nullable: true),
                    EndCidade = table.Column<string>(maxLength: 250, nullable: true),
                    EndEstado = table.Column<string>(maxLength: 250, nullable: true),
                    EndCep = table.Column<string>(maxLength: 250, nullable: true),
                    BancoNome = table.Column<string>(maxLength: 250, nullable: true),
                    BancoAgencia = table.Column<string>(maxLength: 250, nullable: true),
                    BancoOperacao = table.Column<string>(maxLength: 250, nullable: true),
                    BancoConta = table.Column<string>(maxLength: 250, nullable: true),
                    BancoTitular = table.Column<string>(maxLength: 250, nullable: true),
                    BancoDocumento = table.Column<string>(maxLength: 250, nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Remetente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RazaoSocial = table.Column<string>(maxLength: 250, nullable: true),
                    NomeFantasia = table.Column<string>(maxLength: 250, nullable: true),
                    Documento1 = table.Column<string>(maxLength: 250, nullable: true),
                    Documento2 = table.Column<string>(maxLength: 250, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    Contato = table.Column<string>(maxLength: 250, nullable: true),
                    Telefone1 = table.Column<string>(maxLength: 250, nullable: true),
                    Telefone2 = table.Column<string>(maxLength: 250, nullable: true),
                    EndRua = table.Column<string>(maxLength: 250, nullable: true),
                    EndNumero = table.Column<string>(maxLength: 250, nullable: true),
                    EndComplemento = table.Column<string>(maxLength: 250, nullable: true),
                    EndBairro = table.Column<string>(maxLength: 250, nullable: true),
                    EndCidade = table.Column<string>(maxLength: 250, nullable: true),
                    EndEstado = table.Column<string>(maxLength: 250, nullable: true),
                    EndCep = table.Column<string>(maxLength: 250, nullable: true),
                    BancoNome = table.Column<string>(maxLength: 250, nullable: true),
                    BancoAgencia = table.Column<string>(maxLength: 250, nullable: true),
                    BancoOperacao = table.Column<string>(maxLength: 250, nullable: true),
                    BancoConta = table.Column<string>(maxLength: 250, nullable: true),
                    BancoTitular = table.Column<string>(maxLength: 250, nullable: true),
                    BancoDocumento = table.Column<string>(maxLength: 250, nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remetente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 250, nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProprietarioAnexo",
                columns: table => new
                {
                    IdProprietario = table.Column<int>(nullable: false),
                    IdAnexo = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProprietarioAnexo", x => new { x.IdProprietario, x.IdAnexo });
                    table.ForeignKey(
                        name: "FK_ProprietarioAnexo_Anexo_IdAnexo",
                        column: x => x.IdAnexo,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProprietarioAnexo_Proprietario_IdProprietario",
                        column: x => x.IdProprietario,
                        principalTable: "Proprietario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemetenteAnexo",
                columns: table => new
                {
                    IdRemetente = table.Column<int>(nullable: false),
                    IdAnexo = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemetenteAnexo", x => new { x.IdRemetente, x.IdAnexo });
                    table.ForeignKey(
                        name: "FK_RemetenteAnexo_Anexo_IdAnexo",
                        column: x => x.IdAnexo,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RemetenteAnexo_Remetente_IdRemetente",
                        column: x => x.IdRemetente,
                        principalTable: "Remetente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdRemetente",
                table: "Pedido",
                column: "IdRemetente");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_IdCentroCusto",
                table: "Lancamento",
                column: "IdCentroCusto");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_IdContaBancaria",
                table: "Lancamento",
                column: "IdContaBancaria");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_IdTipoDocumento",
                table: "Lancamento",
                column: "IdTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_Caminhao_IdProprietario",
                table: "Caminhao",
                column: "IdProprietario");

            migrationBuilder.CreateIndex(
                name: "IX_ProprietarioAnexo_IdAnexo",
                table: "ProprietarioAnexo",
                column: "IdAnexo");

            migrationBuilder.CreateIndex(
                name: "IX_RemetenteAnexo_IdAnexo",
                table: "RemetenteAnexo",
                column: "IdAnexo");

            migrationBuilder.AddForeignKey(
                name: "FK_Caminhao_Proprietario_IdProprietario",
                table: "Caminhao",
                column: "IdProprietario",
                principalTable: "Proprietario",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Lancamento_CentroCusto_IdCentroCusto",
                table: "Lancamento",
                column: "IdCentroCusto",
                principalTable: "CentroCusto",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Lancamento_ContaBancaria_IdContaBancaria",
                table: "Lancamento",
                column: "IdContaBancaria",
                principalTable: "ContaBancaria",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Lancamento_TipoDocumento_IdTipoDocumento",
                table: "Lancamento",
                column: "IdTipoDocumento",
                principalTable: "TipoDocumento",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Remetente_IdRemetente",
                table: "Pedido",
                column: "IdRemetente",
                principalTable: "Remetente",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caminhao_Proprietario_IdProprietario",
                table: "Caminhao");

            migrationBuilder.DropForeignKey(
                name: "FK_Lancamento_CentroCusto_IdCentroCusto",
                table: "Lancamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Lancamento_ContaBancaria_IdContaBancaria",
                table: "Lancamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Lancamento_TipoDocumento_IdTipoDocumento",
                table: "Lancamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Remetente_IdRemetente",
                table: "Pedido");

            migrationBuilder.DropTable(
                name: "CentroCusto");

            migrationBuilder.DropTable(
                name: "ContaBancaria");

            migrationBuilder.DropTable(
                name: "ProprietarioAnexo");

            migrationBuilder.DropTable(
                name: "RemetenteAnexo");

            migrationBuilder.DropTable(
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "Proprietario");

            migrationBuilder.DropTable(
                name: "Remetente");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_IdRemetente",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Lancamento_IdCentroCusto",
                table: "Lancamento");

            migrationBuilder.DropIndex(
                name: "IX_Lancamento_IdContaBancaria",
                table: "Lancamento");

            migrationBuilder.DropIndex(
                name: "IX_Lancamento_IdTipoDocumento",
                table: "Lancamento");

            migrationBuilder.DropIndex(
                name: "IX_Caminhao_IdProprietario",
                table: "Caminhao");

            migrationBuilder.DropColumn(
                name: "CTe",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "IdRemetente",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "NFe",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "BancoAgencia",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "BancoConta",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "BancoDocumento",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "BancoNome",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "BancoOperacao",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "BancoTitular",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "DataHabilitacao",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "DataVencimento",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "EndBairro",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "EndCep",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "EndCidade",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "EndComplemento",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "EndEstado",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "EndNumero",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "EndRua",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "BancoAgencia",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "BancoConta",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "BancoDocumento",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "BancoNome",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "BancoOperacao",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "BancoTitular",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "IdCentroCusto",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "IdContaBancaria",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "IdTipoDocumento",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "BancoAgencia",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "BancoConta",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "BancoDocumento",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "BancoNome",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "BancoOperacao",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "BancoTitular",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Caminhao");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Caminhao");

            migrationBuilder.DropColumn(
                name: "IdProprietario",
                table: "Caminhao");

            migrationBuilder.DropColumn(
                name: "Placa2",
                table: "Caminhao");

            migrationBuilder.DropColumn(
                name: "Placa3",
                table: "Caminhao");

            migrationBuilder.DropColumn(
                name: "Placa4",
                table: "Caminhao");

            migrationBuilder.DropColumn(
                name: "Renavam",
                table: "Caminhao");
        }
    }
}

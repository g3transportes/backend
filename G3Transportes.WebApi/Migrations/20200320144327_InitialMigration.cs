using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G3Transportes.WebApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anexo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 250, nullable: true),
                    Arquivo = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
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
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configuracao",
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
                    Logomarca = table.Column<string>(maxLength: 250, nullable: true),
                    EndRua = table.Column<string>(maxLength: 250, nullable: true),
                    EndNumero = table.Column<string>(maxLength: 250, nullable: true),
                    EndComplemento = table.Column<string>(maxLength: 250, nullable: true),
                    EndBairro = table.Column<string>(maxLength: 250, nullable: true),
                    EndCidade = table.Column<string>(maxLength: 250, nullable: true),
                    EndEstado = table.Column<string>(maxLength: 250, nullable: true),
                    EndCep = table.Column<string>(maxLength: 250, nullable: true),
                    SmtpHost = table.Column<string>(maxLength: 250, nullable: true),
                    SmtpUsuario = table.Column<string>(maxLength: 250, nullable: true),
                    SmtpSenha = table.Column<string>(maxLength: 250, nullable: true),
                    SmtpPorta = table.Column<int>(nullable: false),
                    SmtpSSL = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormaPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 250, nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorista",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 250, nullable: true),
                    Documento1 = table.Column<string>(maxLength: 250, nullable: true),
                    Documento2 = table.Column<string>(maxLength: 250, nullable: true),
                    Documento3 = table.Column<string>(maxLength: 250, nullable: true),
                    Telefone1 = table.Column<string>(maxLength: 250, nullable: true),
                    Telefone2 = table.Column<string>(maxLength: 250, nullable: true),
                    Telefone3 = table.Column<string>(maxLength: 250, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorista", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 250, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    Senha = table.Column<string>(maxLength: 250, nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClienteAnexo",
                columns: table => new
                {
                    IdCliente = table.Column<int>(nullable: false),
                    IdAnexo = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteAnexo", x => new { x.IdCliente, x.IdAnexo });
                    table.ForeignKey(
                        name: "FK_ClienteAnexo_Anexo_IdAnexo",
                        column: x => x.IdAnexo,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteAnexo_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Caminhao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdMotorista = table.Column<int>(nullable: true),
                    Nome = table.Column<string>(maxLength: 250, nullable: true),
                    Placa = table.Column<string>(maxLength: 250, nullable: true),
                    Modelo = table.Column<string>(maxLength: 250, nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caminhao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caminhao_Motorista_IdMotorista",
                        column: x => x.IdMotorista,
                        principalTable: "Motorista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MotoristaAnexo",
                columns: table => new
                {
                    IdMotorista = table.Column<int>(nullable: false),
                    IdAnexo = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotoristaAnexo", x => new { x.IdMotorista, x.IdAnexo });
                    table.ForeignKey(
                        name: "FK_MotoristaAnexo_Anexo_IdAnexo",
                        column: x => x.IdAnexo,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotoristaAnexo_Motorista_IdMotorista",
                        column: x => x.IdMotorista,
                        principalTable: "Motorista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaminhaoAnexo",
                columns: table => new
                {
                    IdCaminhao = table.Column<int>(nullable: false),
                    IdAnexo = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaminhaoAnexo", x => new { x.IdCaminhao, x.IdAnexo });
                    table.ForeignKey(
                        name: "FK_CaminhaoAnexo_Anexo_IdAnexo",
                        column: x => x.IdAnexo,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaminhaoAnexo_Caminhao_IdCaminhao",
                        column: x => x.IdCaminhao,
                        principalTable: "Caminhao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdCaminhao = table.Column<int>(nullable: false),
                    IdCliente = table.Column<int>(nullable: false),
                    OrdemServico = table.Column<string>(maxLength: 250, nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Destinatario = table.Column<string>(type: "text", nullable: true),
                    LocalColeta = table.Column<string>(type: "text", nullable: true),
                    DataColeta = table.Column<DateTime>(nullable: false),
                    DataFinalizado = table.Column<DateTime>(nullable: false),
                    Quantidade = table.Column<double>(nullable: false),
                    ValorUnitario = table.Column<double>(nullable: false),
                    ValorBruto = table.Column<double>(nullable: false),
                    ValorFrete = table.Column<double>(nullable: false),
                    ValorComissao = table.Column<double>(nullable: false),
                    ValorLiquido = table.Column<double>(nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    Finalizado = table.Column<bool>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_Caminhao_IdCaminhao",
                        column: x => x.IdCaminhao,
                        principalTable: "Caminhao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedido_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lancamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdPedido = table.Column<int>(nullable: true),
                    IdCliente = table.Column<int>(nullable: true),
                    IdCaminhao = table.Column<int>(nullable: true),
                    IdFormaPagamento = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(maxLength: 5, nullable: true),
                    Favorecido = table.Column<string>(maxLength: 250, nullable: true),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    DataBaixa = table.Column<DateTime>(nullable: false),
                    ValorBruto = table.Column<double>(nullable: false),
                    ValorDesconto = table.Column<double>(nullable: false),
                    ValorAcrescimo = table.Column<double>(nullable: false),
                    ValorLiquido = table.Column<double>(nullable: false),
                    ValorBaixado = table.Column<double>(nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    Baixado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lancamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lancamento_Caminhao_IdCaminhao",
                        column: x => x.IdCaminhao,
                        principalTable: "Caminhao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Lancamento_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Lancamento_FormaPagamento_IdFormaPagamento",
                        column: x => x.IdFormaPagamento,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lancamento_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PedidoAnexo",
                columns: table => new
                {
                    IdPedido = table.Column<int>(nullable: false),
                    IdAnexo = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoAnexo", x => new { x.IdPedido, x.IdAnexo });
                    table.ForeignKey(
                        name: "FK_PedidoAnexo_Anexo_IdAnexo",
                        column: x => x.IdAnexo,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoAnexo_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caminhao_IdMotorista",
                table: "Caminhao",
                column: "IdMotorista");

            migrationBuilder.CreateIndex(
                name: "IX_CaminhaoAnexo_IdAnexo",
                table: "CaminhaoAnexo",
                column: "IdAnexo");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteAnexo_IdAnexo",
                table: "ClienteAnexo",
                column: "IdAnexo");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_IdCaminhao",
                table: "Lancamento",
                column: "IdCaminhao");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_IdCliente",
                table: "Lancamento",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_IdFormaPagamento",
                table: "Lancamento",
                column: "IdFormaPagamento");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_IdPedido",
                table: "Lancamento",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_MotoristaAnexo_IdAnexo",
                table: "MotoristaAnexo",
                column: "IdAnexo");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdCaminhao",
                table: "Pedido",
                column: "IdCaminhao");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdCliente",
                table: "Pedido",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoAnexo_IdAnexo",
                table: "PedidoAnexo",
                column: "IdAnexo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaminhaoAnexo");

            migrationBuilder.DropTable(
                name: "ClienteAnexo");

            migrationBuilder.DropTable(
                name: "Configuracao");

            migrationBuilder.DropTable(
                name: "Lancamento");

            migrationBuilder.DropTable(
                name: "MotoristaAnexo");

            migrationBuilder.DropTable(
                name: "PedidoAnexo");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "FormaPagamento");

            migrationBuilder.DropTable(
                name: "Anexo");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Caminhao");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Motorista");
        }
    }
}

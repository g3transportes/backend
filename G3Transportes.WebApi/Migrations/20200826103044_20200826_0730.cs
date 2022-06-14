using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G3Transportes.WebApi.Migrations
{
    public partial class _20200826_0730 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conciliacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdConta = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Saldo = table.Column<double>(type: "double(12,2)", nullable: false),
                    Anexo = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conciliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conciliacao_ContaBancaria_IdConta",
                        column: x => x.IdConta,
                        principalTable: "ContaBancaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conciliacao_IdConta",
                table: "Conciliacao",
                column: "IdConta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conciliacao");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace G3Transportes.WebApi.Migrations
{
    public partial class _20200402_0842 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Capacidade",
                table: "Caminhao",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacidade",
                table: "Caminhao");
        }
    }
}

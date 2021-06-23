using Microsoft.EntityFrameworkCore.Migrations;

namespace Volvo.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnoModelo",
                table: "Caminhao",
                newName: "Ano/Modelo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ano/Modelo",
                table: "Caminhao",
                newName: "AnoModelo");
        }
    }
}

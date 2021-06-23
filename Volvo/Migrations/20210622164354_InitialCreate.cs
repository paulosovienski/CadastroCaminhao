using Microsoft.EntityFrameworkCore.Migrations;

namespace Volvo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caminhao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ano = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnoModelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chassi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaminhaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caminhao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caminhao_Caminhao_CaminhaoId",
                        column: x => x.CaminhaoId,
                        principalTable: "Caminhao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caminhao_CaminhaoId",
                table: "Caminhao",
                column: "CaminhaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caminhao");
        }
    }
}

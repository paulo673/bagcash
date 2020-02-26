using Microsoft.EntityFrameworkCore.Migrations;

namespace bagcash.Migrations
{
    public partial class cartaodoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Cartoes",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cartoes_UsuarioId",
                table: "Cartoes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartoes_AspNetUsers_UsuarioId",
                table: "Cartoes",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartoes_AspNetUsers_UsuarioId",
                table: "Cartoes");

            migrationBuilder.DropIndex(
                name: "IX_Cartoes_UsuarioId",
                table: "Cartoes");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Cartoes");
        }
    }
}

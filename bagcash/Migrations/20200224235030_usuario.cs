using Microsoft.EntityFrameworkCore.Migrations;

namespace bagcash.Migrations
{
    public partial class usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Transacoes",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_UsuarioId",
                table: "Transacoes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_AspNetUsers_UsuarioId",
                table: "Transacoes",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_AspNetUsers_UsuarioId",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_UsuarioId",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Transacoes");
        }
    }
}

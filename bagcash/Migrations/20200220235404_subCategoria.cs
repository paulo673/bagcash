using Microsoft.EntityFrameworkCore.Migrations;

namespace bagcash.Migrations
{
    public partial class subCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Limite",
                table: "Categorias",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoriaId",
                table: "Categorias",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_SubCategoriaId",
                table: "Categorias",
                column: "SubCategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Categorias_SubCategoriaId",
                table: "Categorias",
                column: "SubCategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Categorias_SubCategoriaId",
                table: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_SubCategoriaId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "Limite",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "SubCategoriaId",
                table: "Categorias");
        }
    }
}

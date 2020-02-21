using Microsoft.EntityFrameworkCore.Migrations;

namespace bagcash.Migrations
{
    public partial class categoriaPai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Categorias_SubCategoriaId",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "SubCategoriaId",
                table: "Categorias",
                newName: "CategoriaPaiId");

            migrationBuilder.RenameIndex(
                name: "IX_Categorias_SubCategoriaId",
                table: "Categorias",
                newName: "IX_Categorias_CategoriaPaiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Categorias_CategoriaPaiId",
                table: "Categorias",
                column: "CategoriaPaiId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Categorias_CategoriaPaiId",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "CategoriaPaiId",
                table: "Categorias",
                newName: "SubCategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Categorias_CategoriaPaiId",
                table: "Categorias",
                newName: "IX_Categorias_SubCategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Categorias_SubCategoriaId",
                table: "Categorias",
                column: "SubCategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

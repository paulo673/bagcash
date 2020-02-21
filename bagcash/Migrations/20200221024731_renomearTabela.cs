using Microsoft.EntityFrameworkCore.Migrations;

namespace bagcash.Migrations
{
    public partial class renomearTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fatura_Cartoes_CartaoId",
                table: "Fatura");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcela_Transacoes_TransacaoId",
                table: "Parcela");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Fatura_FaturaId",
                table: "Transacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parcela",
                table: "Parcela");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fatura",
                table: "Fatura");

            migrationBuilder.RenameTable(
                name: "Parcela",
                newName: "Parcelas");

            migrationBuilder.RenameTable(
                name: "Fatura",
                newName: "Faturas");

            migrationBuilder.RenameIndex(
                name: "IX_Parcela_TransacaoId",
                table: "Parcelas",
                newName: "IX_Parcelas_TransacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Fatura_CartaoId",
                table: "Faturas",
                newName: "IX_Faturas_CartaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parcelas",
                table: "Parcelas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faturas",
                table: "Faturas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturas_Cartoes_CartaoId",
                table: "Faturas",
                column: "CartaoId",
                principalTable: "Cartoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcelas_Transacoes_TransacaoId",
                table: "Parcelas",
                column: "TransacaoId",
                principalTable: "Transacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Faturas_FaturaId",
                table: "Transacoes",
                column: "FaturaId",
                principalTable: "Faturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturas_Cartoes_CartaoId",
                table: "Faturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcelas_Transacoes_TransacaoId",
                table: "Parcelas");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Faturas_FaturaId",
                table: "Transacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parcelas",
                table: "Parcelas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faturas",
                table: "Faturas");

            migrationBuilder.RenameTable(
                name: "Parcelas",
                newName: "Parcela");

            migrationBuilder.RenameTable(
                name: "Faturas",
                newName: "Fatura");

            migrationBuilder.RenameIndex(
                name: "IX_Parcelas_TransacaoId",
                table: "Parcela",
                newName: "IX_Parcela_TransacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Faturas_CartaoId",
                table: "Fatura",
                newName: "IX_Fatura_CartaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parcela",
                table: "Parcela",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fatura",
                table: "Fatura",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fatura_Cartoes_CartaoId",
                table: "Fatura",
                column: "CartaoId",
                principalTable: "Cartoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcela_Transacoes_TransacaoId",
                table: "Parcela",
                column: "TransacaoId",
                principalTable: "Transacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Fatura_FaturaId",
                table: "Transacoes",
                column: "FaturaId",
                principalTable: "Fatura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

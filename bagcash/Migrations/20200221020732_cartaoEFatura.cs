using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bagcash.Migrations
{
    public partial class cartaoEFatura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FaturaId",
                table: "Transacoes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cartoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Limite = table.Column<decimal>(nullable: false),
                    DataDeFechamento = table.Column<DateTime>(nullable: false),
                    DataDeVencimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fatura",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataDeFechamento = table.Column<DateTime>(nullable: false),
                    DataDeVencimento = table.Column<DateTime>(nullable: false),
                    CartaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fatura_Cartoes_CartaoId",
                        column: x => x.CartaoId,
                        principalTable: "Cartoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_FaturaId",
                table: "Transacoes",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_CartaoId",
                table: "Fatura",
                column: "CartaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Fatura_FaturaId",
                table: "Transacoes",
                column: "FaturaId",
                principalTable: "Fatura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Fatura_FaturaId",
                table: "Transacoes");

            migrationBuilder.DropTable(
                name: "Fatura");

            migrationBuilder.DropTable(
                name: "Cartoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_FaturaId",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "FaturaId",
                table: "Transacoes");
        }
    }
}

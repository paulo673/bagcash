using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bagcash.Migrations
{
    public partial class parcela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDeEfetivacao",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "Efetivada",
                table: "Transacoes");

            migrationBuilder.CreateTable(
                name: "Parcela",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumeroDaParcela = table.Column<int>(nullable: false),
                    DataDeEfetivacao = table.Column<DateTime>(nullable: false),
                    Efetivada = table.Column<bool>(nullable: false),
                    TransacaoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcela_Transacoes_TransacaoId",
                        column: x => x.TransacaoId,
                        principalTable: "Transacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcela_TransacaoId",
                table: "Parcela",
                column: "TransacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcela");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeEfetivacao",
                table: "Transacoes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Efetivada",
                table: "Transacoes",
                nullable: false,
                defaultValue: false);
        }
    }
}

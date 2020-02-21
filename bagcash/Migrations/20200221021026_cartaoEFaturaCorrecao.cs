using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bagcash.Migrations
{
    public partial class cartaoEFaturaCorrecao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDeFechamento",
                table: "Cartoes");

            migrationBuilder.DropColumn(
                name: "DataDeVencimento",
                table: "Cartoes");

            migrationBuilder.AddColumn<int>(
                name: "DiaDeFechamento",
                table: "Cartoes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiaDeVencimento",
                table: "Cartoes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaDeFechamento",
                table: "Cartoes");

            migrationBuilder.DropColumn(
                name: "DiaDeVencimento",
                table: "Cartoes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeFechamento",
                table: "Cartoes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeVencimento",
                table: "Cartoes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

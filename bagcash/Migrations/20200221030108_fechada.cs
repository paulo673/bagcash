using Microsoft.EntityFrameworkCore.Migrations;

namespace bagcash.Migrations
{
    public partial class fechada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Fechada",
                table: "Faturas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fechada",
                table: "Faturas");
        }
    }
}

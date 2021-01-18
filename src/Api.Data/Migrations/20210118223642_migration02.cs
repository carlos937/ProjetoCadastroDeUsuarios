using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class migration02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "lixeira",
                table: "Usuario",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lixeira",
                table: "Usuario");
        }
    }
}

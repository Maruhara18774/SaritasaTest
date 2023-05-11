using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saritasa.Data.Migrations
{
    public partial class addFileState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileState",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileState",
                table: "Files");
        }
    }
}

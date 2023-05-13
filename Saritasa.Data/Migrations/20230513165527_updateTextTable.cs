using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saritasa.Data.Migrations
{
    public partial class updateTextTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Files");

            migrationBuilder.CreateTable(
                name: "Texts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DownloadOnce = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdateByUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Texts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Texts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Texts_UserID",
                table: "Texts",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Texts");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

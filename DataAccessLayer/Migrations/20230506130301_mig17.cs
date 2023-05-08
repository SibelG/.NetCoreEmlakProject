using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeadingId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_HeadingId",
                table: "Projects",
                column: "HeadingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Headings_HeadingId",
                table: "Projects",
                column: "HeadingId",
                principalTable: "Headings",
                principalColumn: "HeadingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Headings_HeadingId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_HeadingId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "HeadingId",
                table: "Projects");
        }
    }
}

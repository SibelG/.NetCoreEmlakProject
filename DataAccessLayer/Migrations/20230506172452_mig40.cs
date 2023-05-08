using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Projects_DistrictId",
                table: "Projects",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Districts_DistrictId",
                table: "Projects",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "DistrictId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Districts_DistrictId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DistrictId",
                table: "Projects");
        }
    }
}

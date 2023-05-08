using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig43 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectImages_Projects_ProjectId",
                table: "ProjectImages");

            migrationBuilder.DropIndex(
                name: "IX_ProjectImages_ProjectId",
                table: "ProjectImages");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectImages");

            migrationBuilder.AddColumn<int>(
                name: "ProjectsProjectId",
                table: "ProjectImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_ProjectsProjectId",
                table: "ProjectImages",
                column: "ProjectsProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectImages_Projects_ProjectsProjectId",
                table: "ProjectImages",
                column: "ProjectsProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectImages_Projects_ProjectsProjectId",
                table: "ProjectImages");

            migrationBuilder.DropIndex(
                name: "IX_ProjectImages_ProjectsProjectId",
                table: "ProjectImages");

            migrationBuilder.DropColumn(
                name: "ProjectsProjectId",
                table: "ProjectImages");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ProjectImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_ProjectId",
                table: "ProjectImages",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectImages_Projects_ProjectId",
                table: "ProjectImages",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

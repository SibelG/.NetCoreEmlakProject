using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_Situations_SituationId",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_SituationId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "SituationId",
                table: "Types");

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_SituationId",
                table: "Adverts",
                column: "SituationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Situations_SituationId",
                table: "Adverts",
                column: "SituationId",
                principalTable: "Situations",
                principalColumn: "SituationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Situations_SituationId",
                table: "Adverts");

            migrationBuilder.DropIndex(
                name: "IX_Adverts_SituationId",
                table: "Adverts");

            migrationBuilder.AddColumn<int>(
                name: "SituationId",
                table: "Types",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Types_SituationId",
                table: "Types",
                column: "SituationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Situations_SituationId",
                table: "Types",
                column: "SituationId",
                principalTable: "Situations",
                principalColumn: "SituationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

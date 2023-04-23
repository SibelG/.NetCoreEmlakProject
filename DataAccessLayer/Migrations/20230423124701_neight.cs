using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class neight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Neighbourhoods_NeighbourhoodNeiggbourhoodId",
                table: "Adverts");

            migrationBuilder.RenameColumn(
                name: "NeiggbourhoodId",
                table: "Neighbourhoods",
                newName: "NeighbourhoodId");

            migrationBuilder.RenameColumn(
                name: "NeighbourhoodNeiggbourhoodId",
                table: "Adverts",
                newName: "NeighbourhoodId");

            migrationBuilder.RenameIndex(
                name: "IX_Adverts_NeighbourhoodNeiggbourhoodId",
                table: "Adverts",
                newName: "IX_Adverts_NeighbourhoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Neighbourhoods_NeighbourhoodId",
                table: "Adverts",
                column: "NeighbourhoodId",
                principalTable: "Neighbourhoods",
                principalColumn: "NeighbourhoodId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Neighbourhoods_NeighbourhoodId",
                table: "Adverts");

            migrationBuilder.RenameColumn(
                name: "NeighbourhoodId",
                table: "Neighbourhoods",
                newName: "NeiggbourhoodId");

            migrationBuilder.RenameColumn(
                name: "NeighbourhoodId",
                table: "Adverts",
                newName: "NeighbourhoodNeiggbourhoodId");

            migrationBuilder.RenameIndex(
                name: "IX_Adverts_NeighbourhoodId",
                table: "Adverts",
                newName: "IX_Adverts_NeighbourhoodNeiggbourhoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Neighbourhoods_NeighbourhoodNeiggbourhoodId",
                table: "Adverts",
                column: "NeighbourhoodNeiggbourhoodId",
                principalTable: "Neighbourhoods",
                principalColumn: "NeiggbourhoodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

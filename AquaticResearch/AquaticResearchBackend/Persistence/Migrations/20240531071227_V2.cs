using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Species_ScientificName",
                table: "Species",
                column: "ScientificName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResearchProjects_Title",
                table: "ResearchProjects",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Name",
                table: "Locations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Name",
                table: "Equipment",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Species_ScientificName",
                table: "Species");

            migrationBuilder.DropIndex(
                name: "IX_ResearchProjects_Title",
                table: "ResearchProjects");

            migrationBuilder.DropIndex(
                name: "IX_Locations_Name",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_Name",
                table: "Equipment");
        }
    }
}

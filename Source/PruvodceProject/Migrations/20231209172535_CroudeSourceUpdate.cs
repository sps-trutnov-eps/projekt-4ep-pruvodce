using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruvodceProject.Migrations
{
    /// <inheritdoc />
    public partial class CroudeSourceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "odpovedAmina",
                table: "CrowdSource",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "odpovedAmina",
                table: "CrowdSource");
        }
    }
}

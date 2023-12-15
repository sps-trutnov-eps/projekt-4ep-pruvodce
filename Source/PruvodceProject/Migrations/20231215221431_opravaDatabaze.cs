using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruvodceProject.Migrations
{
    /// <inheritdoc />
    public partial class opravaDatabaze : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "popis",
                table: "StravovaciZarizeni",
                newName: "Popis");

            migrationBuilder.RenameColumn(
                name: "odkazNaMenu",
                table: "StravovaciZarizeni",
                newName: "OdkazNaMenu");

            migrationBuilder.RenameColumn(
                name: "nazev",
                table: "StravovaciZarizeni",
                newName: "Nazev");

            migrationBuilder.RenameColumn(
                name: "adresa",
                table: "StravovaciZarizeni",
                newName: "Adresa");

            migrationBuilder.AddColumn<string>(
                name: "Typ",
                table: "StravovaciZarizeni",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Typ",
                table: "StravovaciZarizeni");

            migrationBuilder.RenameColumn(
                name: "Popis",
                table: "StravovaciZarizeni",
                newName: "popis");

            migrationBuilder.RenameColumn(
                name: "OdkazNaMenu",
                table: "StravovaciZarizeni",
                newName: "odkazNaMenu");

            migrationBuilder.RenameColumn(
                name: "Nazev",
                table: "StravovaciZarizeni",
                newName: "nazev");

            migrationBuilder.RenameColumn(
                name: "Adresa",
                table: "StravovaciZarizeni",
                newName: "adresa");
        }
    }
}

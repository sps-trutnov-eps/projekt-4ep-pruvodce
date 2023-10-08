using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruvodceProject.Migrations
{
    /// <inheritdoc />
    public partial class NoveModely1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Automaty",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    budova = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    patro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bagety = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automaty", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Budovy",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kodoveOznaceni = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budovy", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StravovaciZarizeni",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    odkazNaMenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    popis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StravovaciZarizeni", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ucebna",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    patro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    druh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucebna", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automaty");

            migrationBuilder.DropTable(
                name: "Budovy");

            migrationBuilder.DropTable(
                name: "StravovaciZarizeni");

            migrationBuilder.DropTable(
                name: "Ucebna");
        }
    }
}

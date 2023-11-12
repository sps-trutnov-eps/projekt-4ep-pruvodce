using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruvodceProject.Migrations
{
    /// <inheritdoc />
    public partial class doplneni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clanek",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_autora = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NadpisClanku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObsahClanku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clanek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrowdSource",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDUzivatele = table.Column<int>(type: "int", nullable: false),
                    nadpis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stav = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    existujici = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSource", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clanek");

            migrationBuilder.DropTable(
                name: "CrowdSource");
        }
    }
}

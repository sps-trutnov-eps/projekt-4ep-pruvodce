using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruvodceProject.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budovy",
                columns: table => new
                {
                    IdBudovy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kodoveOznaceni = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budovy", x => x.IdBudovy);
                });

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
                    mailUzivatele = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nadpis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stav = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    odpovedAmina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    existujici = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrowdSource", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OverovaciUdaje",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    heslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kod = table.Column<int>(type: "int", nullable: false),
                    expirace = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverovaciUdaje", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PrihlasovaciUdaje",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    heslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jeAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrihlasovaciUdaje", x => x.ID);
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
                name: "Automaty",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    budova = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    patro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bagety = table.Column<bool>(type: "bit", nullable: false),
                    budovaIDIdBudovy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automaty", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Automaty_Budovy_budovaIDIdBudovy",
                        column: x => x.budovaIDIdBudovy,
                        principalTable: "Budovy",
                        principalColumn: "IdBudovy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoBudovy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Pripona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudovaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdBudovy1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoBudovy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoBudovy_Budovy_IdBudovy1",
                        column: x => x.IdBudovy1,
                        principalTable: "Budovy",
                        principalColumn: "IdBudovy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ucebna",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    patro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    druh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    budovaIDIdBudovy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucebna", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ucebna_Budovy_budovaIDIdBudovy",
                        column: x => x.budovaIDIdBudovy,
                        principalTable: "Budovy",
                        principalColumn: "IdBudovy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoUcebny",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Pripona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UcebnaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UcebnaIdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoUcebny", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoUcebny_Ucebna_UcebnaIdId",
                        column: x => x.UcebnaIdId,
                        principalTable: "Ucebna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automaty_budovaIDIdBudovy",
                table: "Automaty",
                column: "budovaIDIdBudovy");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBudovy_IdBudovy1",
                table: "PhotoBudovy",
                column: "IdBudovy1");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoUcebny_UcebnaIdId",
                table: "PhotoUcebny",
                column: "UcebnaIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Ucebna_budovaIDIdBudovy",
                table: "Ucebna",
                column: "budovaIDIdBudovy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automaty");

            migrationBuilder.DropTable(
                name: "Clanek");

            migrationBuilder.DropTable(
                name: "CrowdSource");

            migrationBuilder.DropTable(
                name: "OverovaciUdaje");

            migrationBuilder.DropTable(
                name: "PhotoBudovy");

            migrationBuilder.DropTable(
                name: "PhotoUcebny");

            migrationBuilder.DropTable(
                name: "PrihlasovaciUdaje");

            migrationBuilder.DropTable(
                name: "StravovaciZarizeni");

            migrationBuilder.DropTable(
                name: "Ucebna");

            migrationBuilder.DropTable(
                name: "Budovy");
        }
    }
}

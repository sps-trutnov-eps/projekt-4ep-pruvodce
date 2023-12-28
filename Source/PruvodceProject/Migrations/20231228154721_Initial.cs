using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruvodceProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budovy",
                columns: table => new
                {
                    IdBudovy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodoveOznaceni = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    AutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NadpisClanku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObsahClanku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clanek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OverovaciUdaje",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Heslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KodOvereni = table.Column<int>(type: "int", nullable: false),
                    ExpiraceOvereni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverovaciUdaje", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stiznosti",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UzivatelMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nadpis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stav = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminOdpoved = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Existujici = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stiznosti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StravovaciZarizeni",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OdkazNaMenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Popis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StravovaciZarizeni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uzivatele",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Heslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JeAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzivatele", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Automaty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Budova = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bagety = table.Column<bool>(type: "bit", nullable: false),
                    BudovaIdIdBudovy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automaty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Automaty_Budovy_BudovaIdIdBudovy",
                        column: x => x.BudovaIdIdBudovy,
                        principalTable: "Budovy",
                        principalColumn: "IdBudovy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FotoBudovy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pripona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudovaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotoBudovy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FotoBudovy_Budovy_BudovaID",
                        column: x => x.BudovaID,
                        principalTable: "Budovy",
                        principalColumn: "IdBudovy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ucebny",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Druh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poddruh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudovaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucebny", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ucebny_Budovy_BudovaId",
                        column: x => x.BudovaId,
                        principalTable: "Budovy",
                        principalColumn: "IdBudovy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FotoUcebny",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pripona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UcebnaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotoUcebny", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FotoUcebny_Ucebny_UcebnaID",
                        column: x => x.UcebnaID,
                        principalTable: "Ucebny",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automaty_BudovaIdIdBudovy",
                table: "Automaty",
                column: "BudovaIdIdBudovy");

            migrationBuilder.CreateIndex(
                name: "IX_FotoBudovy_BudovaID",
                table: "FotoBudovy",
                column: "BudovaID");

            migrationBuilder.CreateIndex(
                name: "IX_FotoUcebny_UcebnaID",
                table: "FotoUcebny",
                column: "UcebnaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucebny_BudovaId",
                table: "Ucebny",
                column: "BudovaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automaty");

            migrationBuilder.DropTable(
                name: "Clanek");

            migrationBuilder.DropTable(
                name: "FotoBudovy");

            migrationBuilder.DropTable(
                name: "FotoUcebny");

            migrationBuilder.DropTable(
                name: "OverovaciUdaje");

            migrationBuilder.DropTable(
                name: "Stiznosti");

            migrationBuilder.DropTable(
                name: "StravovaciZarizeni");

            migrationBuilder.DropTable(
                name: "Uzivatele");

            migrationBuilder.DropTable(
                name: "Ucebny");

            migrationBuilder.DropTable(
                name: "Budovy");
        }
    }
}

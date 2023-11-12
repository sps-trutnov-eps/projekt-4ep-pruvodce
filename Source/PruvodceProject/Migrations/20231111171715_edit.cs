using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruvodceProject.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photo");

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
                name: "IX_PhotoBudovy_IdBudovy1",
                table: "PhotoBudovy",
                column: "IdBudovy1");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoUcebny_UcebnaIdId",
                table: "PhotoUcebny",
                column: "UcebnaIdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoBudovy");

            migrationBuilder.DropTable(
                name: "PhotoUcebny");

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdBudovy1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UcebnaIdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BudovaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pripona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UcebnaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Budovy_IdBudovy1",
                        column: x => x.IdBudovy1,
                        principalTable: "Budovy",
                        principalColumn: "IdBudovy",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Photo_Ucebna_UcebnaIdId",
                        column: x => x.UcebnaIdId,
                        principalTable: "Ucebna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_IdBudovy1",
                table: "Photo",
                column: "IdBudovy1");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_UcebnaIdId",
                table: "Photo",
                column: "UcebnaIdId");
        }
    }
}

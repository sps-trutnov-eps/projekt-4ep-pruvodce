using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruvodceProject.Migrations
{
    /// <inheritdoc />
    public partial class model_a_vazba_budova_foto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Budovy",
                newName: "IdBudovy");

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Pripona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdBudovy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BudovyIdBudovy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Budovy_BudovyIdBudovy",
                        column: x => x.BudovyIdBudovy,
                        principalTable: "Budovy",
                        principalColumn: "IdBudovy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_BudovyIdBudovy",
                table: "Photo",
                column: "BudovyIdBudovy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.RenameColumn(
                name: "IdBudovy",
                table: "Budovy",
                newName: "ID");
        }
    }
}

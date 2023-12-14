using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruvodceProject.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ucebna_Budovy_budovaIDIdBudovy",
                table: "Ucebna");

            migrationBuilder.DropIndex(
                name: "IX_Ucebna_budovaIDIdBudovy",
                table: "Ucebna");

            migrationBuilder.DropColumn(
                name: "budovaIDIdBudovy",
                table: "Ucebna");

            migrationBuilder.RenameColumn(
                name: "patro",
                table: "Ucebna",
                newName: "Patro");

            migrationBuilder.RenameColumn(
                name: "druh",
                table: "Ucebna",
                newName: "Druh");

            migrationBuilder.RenameColumn(
                name: "idUcebny",
                table: "Ucebna",
                newName: "Nazev");

            migrationBuilder.RenameColumn(
                name: "budovuID",
                table: "Ucebna",
                newName: "BudovaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucebna_BudovaID",
                table: "Ucebna",
                column: "BudovaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ucebna_Budovy_BudovaID",
                table: "Ucebna",
                column: "BudovaID",
                principalTable: "Budovy",
                principalColumn: "IdBudovy",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ucebna_Budovy_BudovaID",
                table: "Ucebna");

            migrationBuilder.DropIndex(
                name: "IX_Ucebna_BudovaID",
                table: "Ucebna");

            migrationBuilder.RenameColumn(
                name: "Patro",
                table: "Ucebna",
                newName: "patro");

            migrationBuilder.RenameColumn(
                name: "Druh",
                table: "Ucebna",
                newName: "druh");

            migrationBuilder.RenameColumn(
                name: "Nazev",
                table: "Ucebna",
                newName: "idUcebny");

            migrationBuilder.RenameColumn(
                name: "BudovaID",
                table: "Ucebna",
                newName: "budovuID");

            migrationBuilder.AddColumn<Guid>(
                name: "budovaIDIdBudovy",
                table: "Ucebna",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Ucebna_budovaIDIdBudovy",
                table: "Ucebna",
                column: "budovaIDIdBudovy");

            migrationBuilder.AddForeignKey(
                name: "FK_Ucebna_Budovy_budovaIDIdBudovy",
                table: "Ucebna",
                column: "budovaIDIdBudovy",
                principalTable: "Budovy",
                principalColumn: "IdBudovy",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

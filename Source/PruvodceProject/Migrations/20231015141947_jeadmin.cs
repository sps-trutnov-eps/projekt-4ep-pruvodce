using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruvodceProject.Migrations
{
    /// <inheritdoc />
    public partial class jeadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "budovaID",
                table: "Ucebna");

            migrationBuilder.DropColumn(
                name: "budovaID",
                table: "Automaty");

            migrationBuilder.AddColumn<Guid>(
                name: "budovaIDID",
                table: "Ucebna",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "budovaIDID",
                table: "Automaty",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Ucebna_budovaIDID",
                table: "Ucebna",
                column: "budovaIDID");

            migrationBuilder.CreateIndex(
                name: "IX_Automaty_budovaIDID",
                table: "Automaty",
                column: "budovaIDID");

            migrationBuilder.AddForeignKey(
                name: "FK_Automaty_Budovy_budovaIDID",
                table: "Automaty",
                column: "budovaIDID",
                principalTable: "Budovy",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ucebna_Budovy_budovaIDID",
                table: "Ucebna",
                column: "budovaIDID",
                principalTable: "Budovy",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Automaty_Budovy_budovaIDID",
                table: "Automaty");

            migrationBuilder.DropForeignKey(
                name: "FK_Ucebna_Budovy_budovaIDID",
                table: "Ucebna");

            migrationBuilder.DropIndex(
                name: "IX_Ucebna_budovaIDID",
                table: "Ucebna");

            migrationBuilder.DropIndex(
                name: "IX_Automaty_budovaIDID",
                table: "Automaty");

            migrationBuilder.DropColumn(
                name: "budovaIDID",
                table: "Ucebna");

            migrationBuilder.DropColumn(
                name: "budovaIDID",
                table: "Automaty");

            migrationBuilder.AddColumn<int>(
                name: "budovaID",
                table: "Ucebna",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "budovaID",
                table: "Automaty",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruvodceProject.Migrations
{
    /// <inheritdoc />
    public partial class zalozeni_databaze : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "prihlasovaci_Udaje",
                columns: table => new
                {
                    heslo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    prihlas_jmeno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prihlasovaci_Udaje", x => x.heslo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prihlasovaci_Udaje");
        }
    }
}

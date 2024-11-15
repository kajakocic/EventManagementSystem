using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekat_kaja.Migrations
{
    /// <inheritdoc />
    public partial class V5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrojMesta",
                table: "REGISTRACIJA",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojMesta",
                table: "REGISTRACIJA");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackGaming.Migrations
{
    /// <inheritdoc />
    public partial class etatdemande : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "etat",
                table: "Demande",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "etat",
                table: "Demande");
        }
    }
}

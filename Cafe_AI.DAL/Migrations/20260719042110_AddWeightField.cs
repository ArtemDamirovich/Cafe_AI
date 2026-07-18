using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe_AI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Dishes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Dishes");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvidentaStudentilor.Migrations
{
    /// <inheritdoc />
    public partial class _20230412 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Curricula",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Curricula",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Curricula");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Curricula");
        }
    }
}

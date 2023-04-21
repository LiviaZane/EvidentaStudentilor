using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvidentaStudentilor.Migrations
{
    /// <inheritdoc />
    public partial class _20042023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Exams");
        }
    }
}

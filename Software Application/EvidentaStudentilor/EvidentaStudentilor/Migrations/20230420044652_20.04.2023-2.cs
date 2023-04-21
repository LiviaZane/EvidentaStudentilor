using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvidentaStudentilor.Migrations
{
    /// <inheritdoc />
    public partial class _200420232 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurriculaId",
                table: "Grades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurriculaId",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CurriculaId",
                table: "Grades",
                column: "CurriculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CurriculaId",
                table: "Exams",
                column: "CurriculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Curricula_CurriculaId",
                table: "Exams",
                column: "CurriculaId",
                principalTable: "Curricula",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Curricula_CurriculaId",
                table: "Grades",
                column: "CurriculaId",
                principalTable: "Curricula",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Curricula_CurriculaId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Curricula_CurriculaId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_CurriculaId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Exams_CurriculaId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CurriculaId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CurriculaId",
                table: "Exams");
        }
    }
}

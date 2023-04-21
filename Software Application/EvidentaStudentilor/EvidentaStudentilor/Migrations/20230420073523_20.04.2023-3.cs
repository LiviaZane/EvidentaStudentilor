using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvidentaStudentilor.Migrations
{
    /// <inheritdoc />
    public partial class _200420233 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormerGrade",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Grades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_ProfileId",
                table: "Grades",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Profiles_ProfileId",
                table: "Grades",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Profiles_ProfileId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_ProfileId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "FormerGrade",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Grades");
        }
    }
}

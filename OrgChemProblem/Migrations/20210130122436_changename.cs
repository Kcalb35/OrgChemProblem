using Microsoft.EntityFrameworkCore.Migrations;

namespace OrgChemProblem.Migrations
{
    public partial class changename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "ProblemPictures",
                "Problems",
                "ProblemPicture");

            migrationBuilder.RenameColumn(
                "AnswerPictures",
                "Problems",
                "AnswerPicture");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "ProblemPicture",
                "Problems",
                "ProblemPictures");

            migrationBuilder.RenameColumn(
                "AnswerPicture",
                "Problems",
                "AnswerPictures");
        }
    }
}
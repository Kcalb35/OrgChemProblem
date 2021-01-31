using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrgChemProblem.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Managers",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: false),
                    HashPassword = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Managers", x => x.Id); });

            migrationBuilder.CreateTable(
                "Problems",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProblemDescription = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: true),
                    ProblemPictures = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: true),
                    AnswerDescription = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: true),
                    AnswerPictures = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: true),
                    Tags = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Problems", x => x.Id); });

            migrationBuilder.InsertData(
                "Managers",
                new[] {"Id", "HashPassword", "UserName"},
                new object[] {1, "fb2d45603825126005378c83049ca239f47e4b6b3e3d97acc259f18d5225781e", "admin"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Managers");

            migrationBuilder.DropTable(
                "Problems");
        }
    }
}
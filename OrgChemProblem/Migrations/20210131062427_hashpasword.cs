using Microsoft.EntityFrameworkCore.Migrations;

namespace OrgChemProblem.Migrations
{
    public partial class hashpasword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashPassword",
                value: "$2a$11$BisBlL6Mud3CugdjTF4u9uRZDc2VEX/rScX7DZ8NDxZ.l6uK0C0cu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashPassword",
                value: "fb2d45603825126005378c83049ca239f47e4b6b3e3d97acc259f18d5225781e");
        }
    }
}

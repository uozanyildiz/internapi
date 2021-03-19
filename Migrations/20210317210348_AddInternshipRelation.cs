using Microsoft.EntityFrameworkCore.Migrations;

namespace internapi.Migrations
{
    public partial class AddInternshipRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Internships_CompanyId",
                table: "Internships",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_StudentId",
                table: "Internships",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_Companies_CompanyId",
                table: "Internships",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Internships_Students_StudentId",
                table: "Internships",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internships_Companies_CompanyId",
                table: "Internships");

            migrationBuilder.DropForeignKey(
                name: "FK_Internships_Students_StudentId",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_Internships_CompanyId",
                table: "Internships");

            migrationBuilder.DropIndex(
                name: "IX_Internships_StudentId",
                table: "Internships");
        }
    }
}

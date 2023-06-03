using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grade_Book_API.Migrations
{
    public partial class UserAndRoleAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Students_StudentId",
                table: "FinalGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Subjects_SubjectId",
                table: "FinalGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinalGrade",
                table: "FinalGrade");

            migrationBuilder.RenameTable(
                name: "FinalGrade",
                newName: "FinalGrades");

            migrationBuilder.RenameIndex(
                name: "IX_FinalGrade_SubjectId",
                table: "FinalGrades",
                newName: "IX_FinalGrades_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalGrade_StudentId",
                table: "FinalGrades",
                newName: "IX_FinalGrades_StudentId");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinalGrades",
                table: "FinalGrades",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_RoleId",
                table: "Students",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrades_Students_StudentId",
                table: "FinalGrades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrades_Subjects_SubjectId",
                table: "FinalGrades",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Roles_RoleId",
                table: "Students",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrades_Students_StudentId",
                table: "FinalGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrades_Subjects_SubjectId",
                table: "FinalGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Roles_RoleId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Students_RoleId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinalGrades",
                table: "FinalGrades");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "FinalGrades",
                newName: "FinalGrade");

            migrationBuilder.RenameIndex(
                name: "IX_FinalGrades_SubjectId",
                table: "FinalGrade",
                newName: "IX_FinalGrade_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalGrades_StudentId",
                table: "FinalGrade",
                newName: "IX_FinalGrade_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinalGrade",
                table: "FinalGrade",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrade_Students_StudentId",
                table: "FinalGrade",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrade_Subjects_SubjectId",
                table: "FinalGrade",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

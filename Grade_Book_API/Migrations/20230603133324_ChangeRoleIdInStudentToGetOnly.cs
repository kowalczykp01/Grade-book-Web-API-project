using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grade_Book_API.Migrations
{
    public partial class ChangeRoleIdInStudentToGetOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Roles_RoleId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Students",
                newName: "RoleId1");

            migrationBuilder.RenameIndex(
                name: "IX_Students_RoleId",
                table: "Students",
                newName: "IX_Students_RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Roles_RoleId1",
                table: "Students",
                column: "RoleId1",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Roles_RoleId1",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "RoleId1",
                table: "Students",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_RoleId1",
                table: "Students",
                newName: "IX_Students_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Roles_RoleId",
                table: "Students",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

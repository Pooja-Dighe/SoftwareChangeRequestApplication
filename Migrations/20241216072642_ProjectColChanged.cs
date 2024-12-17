using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCRSApplication.Migrations
{
    /// <inheritdoc />
    public partial class ProjectColChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEntity_Role_RoleId",
                schema: "Identity",
                table: "ProjectEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamEntity_ProjectEntity_ProjectId1",
                schema: "Identity",
                table: "TeamEntity");

            migrationBuilder.DropIndex(
                name: "IX_TeamEntity_ProjectId1",
                schema: "Identity",
                table: "TeamEntity");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                schema: "Identity",
                table: "TeamEntity");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "Identity",
                table: "ProjectEntity",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEntity_RoleId",
                schema: "Identity",
                table: "ProjectEntity",
                newName: "IX_ProjectEntity_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                schema: "Identity",
                table: "TeamEntity",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamEntity_ProjectId",
                schema: "Identity",
                table: "TeamEntity",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEntity_User_UserId",
                schema: "Identity",
                table: "ProjectEntity",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamEntity_ProjectEntity_ProjectId",
                schema: "Identity",
                table: "TeamEntity",
                column: "ProjectId",
                principalSchema: "Identity",
                principalTable: "ProjectEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectEntity_User_UserId",
                schema: "Identity",
                table: "ProjectEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamEntity_ProjectEntity_ProjectId",
                schema: "Identity",
                table: "TeamEntity");

            migrationBuilder.DropIndex(
                name: "IX_TeamEntity_ProjectId",
                schema: "Identity",
                table: "TeamEntity");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Identity",
                table: "ProjectEntity",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectEntity_UserId",
                schema: "Identity",
                table: "ProjectEntity",
                newName: "IX_ProjectEntity_RoleId");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                schema: "Identity",
                table: "TeamEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId1",
                schema: "Identity",
                table: "TeamEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeamEntity_ProjectId1",
                schema: "Identity",
                table: "TeamEntity",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectEntity_Role_RoleId",
                schema: "Identity",
                table: "ProjectEntity",
                column: "RoleId",
                principalSchema: "Identity",
                principalTable: "Role",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamEntity_ProjectEntity_ProjectId1",
                schema: "Identity",
                table: "TeamEntity",
                column: "ProjectId1",
                principalSchema: "Identity",
                principalTable: "ProjectEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

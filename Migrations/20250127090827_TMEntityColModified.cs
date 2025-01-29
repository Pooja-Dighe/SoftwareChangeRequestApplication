using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCRSApplication.Migrations
{
    /// <inheritdoc />
    public partial class TMEntityColModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMemberEntity_TeamEntity_TeamId",
                schema: "Identity",
                table: "TeamMemberEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMemberEntity_User_UserId",
                schema: "Identity",
                table: "TeamMemberEntity");

            migrationBuilder.DropIndex(
                name: "IX_TeamMemberEntity_UserId",
                schema: "Identity",
                table: "TeamMemberEntity");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Identity",
                table: "TeamMemberEntity");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                schema: "Identity",
                table: "TeamMemberEntity",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamMemberEntity_MemberId",
                schema: "Identity",
                table: "TeamMemberEntity",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMemberEntity_TeamEntity_TeamId",
                schema: "Identity",
                table: "TeamMemberEntity",
                column: "TeamId",
                principalSchema: "Identity",
                principalTable: "TeamEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMemberEntity_User_MemberId",
                schema: "Identity",
                table: "TeamMemberEntity",
                column: "MemberId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMemberEntity_TeamEntity_TeamId",
                schema: "Identity",
                table: "TeamMemberEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMemberEntity_User_MemberId",
                schema: "Identity",
                table: "TeamMemberEntity");

            migrationBuilder.DropIndex(
                name: "IX_TeamMemberEntity_MemberId",
                schema: "Identity",
                table: "TeamMemberEntity");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                schema: "Identity",
                table: "TeamMemberEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Identity",
                table: "TeamMemberEntity",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamMemberEntity_UserId",
                schema: "Identity",
                table: "TeamMemberEntity",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMemberEntity_TeamEntity_TeamId",
                schema: "Identity",
                table: "TeamMemberEntity",
                column: "TeamId",
                principalSchema: "Identity",
                principalTable: "TeamEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMemberEntity_User_UserId",
                schema: "Identity",
                table: "TeamMemberEntity",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}

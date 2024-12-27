using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCRSApplication.Migrations
{
    /// <inheritdoc />
    public partial class RREProjectColModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Project",
                schema: "Identity",
                table: "RaiseRequestEntity");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Identity",
                table: "UserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "Identity",
                table: "UserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                schema: "Identity",
                table: "UserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "Identity",
                table: "UserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RaiseRequestEntity_ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_RaiseRequestEntity_ProjectEntity_ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity",
                column: "ProjectId",
                principalSchema: "Identity",
                principalTable: "ProjectEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaiseRequestEntity_ProjectEntity_ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity");

            migrationBuilder.DropIndex(
                name: "IX_RaiseRequestEntity_ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Identity",
                table: "UserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "Identity",
                table: "UserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                schema: "Identity",
                table: "UserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "Identity",
                table: "UserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Project",
                schema: "Identity",
                table: "RaiseRequestEntity",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

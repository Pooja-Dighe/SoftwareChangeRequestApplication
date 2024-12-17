using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCRSApplication.Migrations
{
    /// <inheritdoc />
    public partial class ProjectTeamsTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                schema: "Identity",
                table: "RaiseRequestEntity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                schema: "Identity",
                table: "RaiseRequestEntity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "Identity",
                table: "RaiseRequestEntity",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Identity",
                table: "RaiseRequestEntity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ProjectEntity",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectEntity_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeamEntity",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamEntity_ProjectEntity_ProjectId1",
                        column: x => x.ProjectId1,
                        principalSchema: "Identity",
                        principalTable: "ProjectEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamEntity_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEntity_RoleId",
                schema: "Identity",
                table: "ProjectEntity",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamEntity_ProjectId1",
                schema: "Identity",
                table: "TeamEntity",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_TeamEntity_UserId",
                schema: "Identity",
                table: "TeamEntity",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamEntity",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ProjectEntity",
                schema: "Identity");

            migrationBuilder.DropColumn(
                name: "AddedAt",
                schema: "Identity",
                table: "RaiseRequestEntity");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "Identity",
                table: "RaiseRequestEntity");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Identity",
                table: "RaiseRequestEntity");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DueDate",
                schema: "Identity",
                table: "RaiseRequestEntity",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}

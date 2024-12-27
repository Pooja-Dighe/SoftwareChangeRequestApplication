using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCRSApplication.Migrations
{
    /// <inheritdoc />
    public partial class RREColRowVersionUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaiseRequestEntity_ProjectEntity_ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity");


            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RaiseRequestEntity_ProjectEntity_ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity",
                column: "ProjectId",
                principalSchema: "Identity",
                principalTable: "ProjectEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaiseRequestEntity_ProjectEntity_ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity");

         
            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RaiseRequestEntity_ProjectEntity_ProjectId",
                schema: "Identity",
                table: "RaiseRequestEntity",
                column: "ProjectId",
                principalSchema: "Identity",
                principalTable: "ProjectEntity",
                principalColumn: "Id");
        }
    }
}

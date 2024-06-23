using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourierApi.Migrations
{
    /// <inheritdoc />
    public partial class jjjdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackHistories_Users_UserId",
                table: "TrackHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Userstr");

            migrationBuilder.AlterColumn<string>(
                name: "Created",
                table: "Userstr",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Userstr",
                table: "Userstr",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackHistories_Userstr_UserId",
                table: "TrackHistories",
                column: "UserId",
                principalTable: "Userstr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackHistories_Userstr_UserId",
                table: "TrackHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Userstr",
                table: "Userstr");

            migrationBuilder.RenameTable(
                name: "Userstr",
                newName: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackHistories_Users_UserId",
                table: "TrackHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

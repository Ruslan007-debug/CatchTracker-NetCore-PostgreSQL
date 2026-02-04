using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatchTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsForAuthorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "PasswordHash");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "users",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Name",
                table: "users",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_Email",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_Name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "users");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "Password");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}

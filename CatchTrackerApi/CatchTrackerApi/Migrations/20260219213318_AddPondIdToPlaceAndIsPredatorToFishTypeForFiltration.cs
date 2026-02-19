using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatchTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPondIdToPlaceAndIsPredatorToFishTypeForFiltration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PondId",
                table: "places",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "fish_types",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPredatory",
                table: "fish_types",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PondId",
                table: "places");

            migrationBuilder.DropColumn(
                name: "IsPredatory",
                table: "fish_types");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "fish_types",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);
        }
    }
}

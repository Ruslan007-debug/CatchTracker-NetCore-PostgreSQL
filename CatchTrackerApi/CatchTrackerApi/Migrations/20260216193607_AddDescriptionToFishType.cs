using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatchTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToFishType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "fish_types",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "fish_types");
        }
    }
}

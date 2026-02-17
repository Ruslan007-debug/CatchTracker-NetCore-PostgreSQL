using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatchTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlAndDescriptionForPlaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "places",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "places",
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
                table: "places");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "places");
        }
    }
}

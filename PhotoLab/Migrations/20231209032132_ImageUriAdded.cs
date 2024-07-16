using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoLab.Migrations
{
    /// <inheritdoc />
    public partial class ImageUriAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURI",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURI",
                table: "Services");
        }
    }
}

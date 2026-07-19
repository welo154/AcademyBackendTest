using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Playlists.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSongFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Songs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Songs",
                newName: "DurationInSeconds");

            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Songs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Songs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DurationInSeconds",
                table: "Songs",
                newName: "Duration");
        }
    }
}

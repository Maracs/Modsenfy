using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modsenfy.DataAccessLayer.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artist_ArtistId",
                table: "Album");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Album",
                newName: "AlbumOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Album_ArtistId",
                table: "Album",
                newName: "IX_Album_AlbumOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artist_AlbumOwnerId",
                table: "Album",
                column: "AlbumOwnerId",
                principalTable: "Artist",
                principalColumn: "ArtistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artist_AlbumOwnerId",
                table: "Album");

            migrationBuilder.RenameColumn(
                name: "AlbumOwnerId",
                table: "Album",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Album_AlbumOwnerId",
                table: "Album",
                newName: "IX_Album_ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artist_ArtistId",
                table: "Album",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

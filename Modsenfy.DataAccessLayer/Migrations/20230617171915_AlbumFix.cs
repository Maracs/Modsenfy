using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modsenfy.DataAccessLayer.Migrations
{
    public partial class AlbumFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_AlbumType_AlbumTypeId",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artist_ArtistId",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Album_Image_CoverId",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Album_AlbumId",
                table: "Tracks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAlbums_Album_AlbumId",
                table: "UserAlbums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.DropIndex(
                name: "IX_Album_ArtistId",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Album");

            migrationBuilder.RenameTable(
                name: "Album",
                newName: "Albums");

            migrationBuilder.RenameIndex(
                name: "IX_Album_CoverId",
                table: "Albums",
                newName: "IX_Albums_CoverId");

            migrationBuilder.RenameIndex(
                name: "IX_Album_AlbumTypeId",
                table: "Albums",
                newName: "IX_Albums_AlbumTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albums",
                table: "Albums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_AlbumOwnerId",
                table: "Albums",
                column: "AlbumOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AlbumType_AlbumTypeId",
                table: "Albums",
                column: "AlbumTypeId",
                principalTable: "AlbumType",
                principalColumn: "AlbumTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artist_AlbumOwnerId",
                table: "Albums",
                column: "AlbumOwnerId",
                principalTable: "Artist",
                principalColumn: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Image_CoverId",
                table: "Albums",
                column: "CoverId",
                principalTable: "Image",
                principalColumn: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Albums_AlbumId",
                table: "Tracks",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAlbums_Albums_AlbumId",
                table: "UserAlbums",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AlbumType_AlbumTypeId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artist_AlbumOwnerId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Image_CoverId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Albums_AlbumId",
                table: "Tracks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAlbums_Albums_AlbumId",
                table: "UserAlbums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albums",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_AlbumOwnerId",
                table: "Albums");

            migrationBuilder.RenameTable(
                name: "Albums",
                newName: "Album");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_CoverId",
                table: "Album",
                newName: "IX_Album_CoverId");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_AlbumTypeId",
                table: "Album",
                newName: "IX_Album_AlbumTypeId");

            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Album",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Album_ArtistId",
                table: "Album",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_AlbumType_AlbumTypeId",
                table: "Album",
                column: "AlbumTypeId",
                principalTable: "AlbumType",
                principalColumn: "AlbumTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artist_ArtistId",
                table: "Album",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Image_CoverId",
                table: "Album",
                column: "CoverId",
                principalTable: "Image",
                principalColumn: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Album_AlbumId",
                table: "Tracks",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAlbums_Album_AlbumId",
                table: "UserAlbums",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

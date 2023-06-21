using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modsenfy.DataAccessLayer.Migrations
{
	public partial class mergedV : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Albums_Image_CoverId",
				table: "Albums");

			migrationBuilder.DropForeignKey(
				name: "FK_Artist_Image_ImageId",
				table: "Artist");

			migrationBuilder.DropForeignKey(
				name: "FK_Image_ImageType_ImageTypeId",
				table: "Image");

			migrationBuilder.DropForeignKey(
				name: "FK_Playlist_Image_CoverId",
				table: "Playlist");

			migrationBuilder.DropForeignKey(
				name: "FK_Playlist_User_UserId",
				table: "Playlist");

			migrationBuilder.DropForeignKey(
				name: "FK_Requests_Image_ImageId",
				table: "Requests");

			migrationBuilder.DropForeignKey(
				name: "FK_Requests_RequestStatus_RequestStatusId",
				table: "Requests");

			migrationBuilder.DropForeignKey(
				name: "FK_Requests_User_UserId",
				table: "Requests");

			migrationBuilder.DropForeignKey(
				name: "FK_Stream_Tracks_TrackId",
				table: "Stream");

			migrationBuilder.DropForeignKey(
				name: "FK_Stream_User_UserId",
				table: "Stream");

			migrationBuilder.DropForeignKey(
				name: "FK_User_Role_RoleId",
				table: "User");

			migrationBuilder.DropForeignKey(
				name: "FK_User_UserInfo_UserInfoId",
				table: "User");

			migrationBuilder.DropForeignKey(
				name: "FK_UserAlbums_User_UserId",
				table: "UserAlbums");

			migrationBuilder.DropForeignKey(
				name: "FK_UserArtists_User_UserId",
				table: "UserArtists");

			migrationBuilder.DropForeignKey(
				name: "FK_UserInfo_Image_ImageId",
				table: "UserInfo");

			migrationBuilder.DropForeignKey(
				name: "FK_UserPlaylists_User_UserId",
				table: "UserPlaylists");

			migrationBuilder.DropForeignKey(
				name: "FK_UserTracks_User_UserId",
				table: "UserTracks");

			migrationBuilder.DropIndex(
				name: "IX_Playlist_UserId",
				table: "Playlist");

			migrationBuilder.DropPrimaryKey(
				name: "PK_UserInfo",
				table: "UserInfo");

			migrationBuilder.DropPrimaryKey(
				name: "PK_User",
				table: "User");

			migrationBuilder.DropIndex(
				name: "IX_User_RoleId",
				table: "User");

			migrationBuilder.DropIndex(
				name: "IX_User_UserInfoId",
				table: "User");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Stream",
				table: "Stream");

			migrationBuilder.DropPrimaryKey(
				name: "PK_RequestStatus",
				table: "RequestStatus");

			migrationBuilder.DropPrimaryKey(
				name: "PK_ImageType",
				table: "ImageType");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Image",
				table: "Image");

			migrationBuilder.DropColumn(
				name: "RequestTimeCreated",
				table: "Requests");

			migrationBuilder.DropColumn(
				name: "UserId",
				table: "Playlist");

			migrationBuilder.DropColumn(
				name: "RoleId",
				table: "User");

			migrationBuilder.RenameTable(
				name: "UserInfo",
				newName: "UserInfos");

			migrationBuilder.RenameTable(
				name: "User",
				newName: "Users");

			migrationBuilder.RenameTable(
				name: "Stream",
				newName: "Streams");

			migrationBuilder.RenameTable(
				name: "RequestStatus",
				newName: "RequestStatuses");

			migrationBuilder.RenameTable(
				name: "ImageType",
				newName: "ImageTypes");

			migrationBuilder.RenameTable(
				name: "Image",
				schema: "dbo",
				newName: "Images",
				newSchema: "dbo");
				

			migrationBuilder.RenameColumn(
				name: "RequestTimeProcessed",
				table: "Requests",
				newName: "RequestTime");

			migrationBuilder.RenameIndex(
				name: "IX_UserInfo_ImageId",
				table: "UserInfos",
				newName: "IX_UserInfos_ImageId");

			migrationBuilder.RenameIndex(
				name: "IX_Stream_TrackId",
				table: "Streams",
				newName: "IX_Streams_TrackId");

			migrationBuilder.RenameIndex(
				name: "IX_Image_ImageTypeId",
				table: "Images",
				newName: "IX_Images_ImageTypeId");

			migrationBuilder.AlterColumn<DateTime>(
				name: "UserPlaylistsAdded",
				table: "UserPlaylists",
				type: "datetime2",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)");

			migrationBuilder.AddColumn<DateTime>(
				name: "UserAlbumsAdded",
				table: "UserAlbums",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddPrimaryKey(
				name: "PK_UserInfos",
				table: "UserInfos",
				column: "UserInfoId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Users",
				table: "Users",
				column: "UserId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Streams",
				table: "Streams",
				columns: new[] { "UserId", "TrackId", "StreamDate" });

			migrationBuilder.AddPrimaryKey(
				name: "PK_RequestStatuses",
				table: "RequestStatuses",
				column: "RequestStatusId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_ImageTypes",
				table: "ImageTypes",
				column: "ImageTypeId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Images",
				table: "Images",
				column: "ImageId");

			migrationBuilder.CreateIndex(
				name: "IX_Playlist_PlaylistOwnerId",
				table: "Playlist",
				column: "PlaylistOwnerId");

			migrationBuilder.CreateIndex(
				name: "IX_Users_UserInfoId",
				table: "Users",
				column: "UserInfoId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Users_UserRoleId",
				table: "Users",
				column: "UserRoleId");

			migrationBuilder.AddForeignKey(
				name: "FK_Albums_Images_CoverId",
				table: "Albums",
				column: "CoverId",
				principalTable: "Images",
				principalColumn: "ImageId");

			migrationBuilder.AddForeignKey(
				name: "FK_Artist_Images_ImageId",
				table: "Artist",
				column: "ImageId",
				principalTable: "Images",
				principalColumn: "ImageId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Images_ImageTypes_ImageTypeId",
				table: "Images",
				column: "ImageTypeId",
				principalTable: "ImageTypes",
				principalColumn: "ImageTypeId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Playlist_Images_CoverId",
				table: "Playlist",
				column: "CoverId",
				principalTable: "Images",
				principalColumn: "ImageId");

			migrationBuilder.AddForeignKey(
				name: "FK_Playlist_Users_PlaylistOwnerId",
				table: "Playlist",
				column: "PlaylistOwnerId",
				principalTable: "Users",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Requests_Images_ImageId",
				table: "Requests",
				column: "ImageId",
				principalTable: "Images",
				principalColumn: "ImageId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Requests_RequestStatuses_RequestStatusId",
				table: "Requests",
				column: "RequestStatusId",
				principalTable: "RequestStatuses",
				principalColumn: "RequestStatusId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Requests_Users_UserId",
				table: "Requests",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Streams_Tracks_TrackId",
				table: "Streams",
				column: "TrackId",
				principalTable: "Tracks",
				principalColumn: "TrackId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Streams_Users_UserId",
				table: "Streams",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_UserAlbums_Users_UserId",
				table: "UserAlbums",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_UserArtists_Users_UserId",
				table: "UserArtists",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_UserInfos_Images_ImageId",
				table: "UserInfos",
				column: "ImageId",
				principalTable: "Images",
				principalColumn: "ImageId");

			migrationBuilder.AddForeignKey(
				name: "FK_UserPlaylists_Users_UserId",
				table: "UserPlaylists",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "UserId",
				onDelete: ReferentialAction.NoAction);

			migrationBuilder.AddForeignKey(
				name: "FK_Users_Role_UserRoleId",
				table: "Users",
				column: "UserRoleId",
				principalTable: "Role",
				principalColumn: "RoleId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Users_UserInfos_UserInfoId",
				table: "Users",
				column: "UserInfoId",
				principalTable: "UserInfos",
				principalColumn: "UserInfoId");

			migrationBuilder.AddForeignKey(
				name: "FK_UserTracks_Users_UserId",
				table: "UserTracks",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Albums_Images_CoverId",
				table: "Albums");

			migrationBuilder.DropForeignKey(
				name: "FK_Artist_Images_ImageId",
				table: "Artist");

			migrationBuilder.DropForeignKey(
				name: "FK_Images_ImageTypes_ImageTypeId",
				table: "Images");

			migrationBuilder.DropForeignKey(
				name: "FK_Playlist_Images_CoverId",
				table: "Playlist");

			migrationBuilder.DropForeignKey(
				name: "FK_Playlist_Users_PlaylistOwnerId",
				table: "Playlist");

			migrationBuilder.DropForeignKey(
				name: "FK_Requests_Images_ImageId",
				table: "Requests");

			migrationBuilder.DropForeignKey(
				name: "FK_Requests_RequestStatuses_RequestStatusId",
				table: "Requests");

			migrationBuilder.DropForeignKey(
				name: "FK_Requests_Users_UserId",
				table: "Requests");

			migrationBuilder.DropForeignKey(
				name: "FK_Streams_Tracks_TrackId",
				table: "Streams");

			migrationBuilder.DropForeignKey(
				name: "FK_Streams_Users_UserId",
				table: "Streams");

			migrationBuilder.DropForeignKey(
				name: "FK_UserAlbums_Users_UserId",
				table: "UserAlbums");

			migrationBuilder.DropForeignKey(
				name: "FK_UserArtists_Users_UserId",
				table: "UserArtists");

			migrationBuilder.DropForeignKey(
				name: "FK_UserInfos_Images_ImageId",
				table: "UserInfos");

			migrationBuilder.DropForeignKey(
				name: "FK_UserPlaylists_Users_UserId",
				table: "UserPlaylists");

			migrationBuilder.DropForeignKey(
				name: "FK_Users_Role_UserRoleId",
				table: "Users");

			migrationBuilder.DropForeignKey(
				name: "FK_Users_UserInfos_UserInfoId",
				table: "Users");

			migrationBuilder.DropForeignKey(
				name: "FK_UserTracks_Users_UserId",
				table: "UserTracks");

			migrationBuilder.DropIndex(
				name: "IX_Playlist_PlaylistOwnerId",
				table: "Playlist");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Users",
				table: "Users");

			migrationBuilder.DropIndex(
				name: "IX_Users_UserInfoId",
				table: "Users");

			migrationBuilder.DropIndex(
				name: "IX_Users_UserRoleId",
				table: "Users");

			migrationBuilder.DropPrimaryKey(
				name: "PK_UserInfos",
				table: "UserInfos");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Streams",
				table: "Streams");

			migrationBuilder.DropPrimaryKey(
				name: "PK_RequestStatuses",
				table: "RequestStatuses");

			migrationBuilder.DropPrimaryKey(
				name: "PK_ImageTypes",
				table: "ImageTypes");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Images",
				table: "Images");

			migrationBuilder.DropColumn(
				name: "UserAlbumsAdded",
				table: "UserAlbums");

			migrationBuilder.RenameTable(
				name: "Users",
				newName: "User");

			migrationBuilder.RenameTable(
				name: "UserInfos",
				newName: "UserInfo");

			migrationBuilder.RenameTable(
				name: "Streams",
				newName: "Stream");

			migrationBuilder.RenameTable(
				name: "RequestStatuses",
				newName: "RequestStatus");

			migrationBuilder.RenameTable(
				name: "ImageTypes",
				newName: "ImageType");

			migrationBuilder.RenameTable(
				name: "Images",
				newName: "Image");

			migrationBuilder.RenameColumn(
				name: "RequestTime",
				table: "Requests",
				newName: "RequestTimeProcessed");

			migrationBuilder.RenameIndex(
				name: "IX_UserInfos_ImageId",
				table: "UserInfo",
				newName: "IX_UserInfo_ImageId");

			migrationBuilder.RenameIndex(
				name: "IX_Streams_TrackId",
				table: "Stream",
				newName: "IX_Stream_TrackId");

			migrationBuilder.RenameIndex(
				name: "IX_Images_ImageTypeId",
				table: "Image",
				newName: "IX_Image_ImageTypeId");

			migrationBuilder.AlterColumn<string>(
				name: "UserPlaylistsAdded",
				table: "UserPlaylists",
				type: "nvarchar(max)",
				nullable: false,
				oldClrType: typeof(DateTime),
				oldType: "datetime2");

			migrationBuilder.AddColumn<DateTime>(
				name: "RequestTimeCreated",
				table: "Requests",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddColumn<int>(
				name: "UserId",
				table: "Playlist",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<int>(
				name: "RoleId",
				table: "User",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddPrimaryKey(
				name: "PK_User",
				table: "User",
				column: "UserId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_UserInfo",
				table: "UserInfo",
				column: "UserInfoId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Stream",
				table: "Stream",
				columns: new[] { "UserId", "TrackId" });

			migrationBuilder.AddPrimaryKey(
				name: "PK_RequestStatus",
				table: "RequestStatus",
				column: "RequestStatusId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_ImageType",
				table: "ImageType",
				column: "ImageTypeId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Image",
				table: "Image",
				column: "ImageId");

			migrationBuilder.CreateIndex(
				name: "IX_Playlist_UserId",
				table: "Playlist",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_User_RoleId",
				table: "User",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "IX_User_UserInfoId",
				table: "User",
				column: "UserInfoId");

			migrationBuilder.AddForeignKey(
				name: "FK_Albums_Image_CoverId",
				table: "Albums",
				column: "CoverId",
				principalTable: "Image",
				principalColumn: "ImageId");

			migrationBuilder.AddForeignKey(
				name: "FK_Artist_Image_ImageId",
				table: "Artist",
				column: "ImageId",
				principalTable: "Image",
				principalColumn: "ImageId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Image_ImageType_ImageTypeId",
				table: "Image",
				column: "ImageTypeId",
				principalTable: "ImageType",
				principalColumn: "ImageTypeId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Playlist_Image_CoverId",
				table: "Playlist",
				column: "CoverId",
				principalTable: "Image",
				principalColumn: "ImageId");

			migrationBuilder.AddForeignKey(
				name: "FK_Playlist_User_UserId",
				table: "Playlist",
				column: "UserId",
				principalTable: "User",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Requests_Image_ImageId",
				table: "Requests",
				column: "ImageId",
				principalTable: "Image",
				principalColumn: "ImageId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Requests_RequestStatus_RequestStatusId",
				table: "Requests",
				column: "RequestStatusId",
				principalTable: "RequestStatus",
				principalColumn: "RequestStatusId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Requests_User_UserId",
				table: "Requests",
				column: "UserId",
				principalTable: "User",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Stream_Tracks_TrackId",
				table: "Stream",
				column: "TrackId",
				principalTable: "Tracks",
				principalColumn: "TrackId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Stream_User_UserId",
				table: "Stream",
				column: "UserId",
				principalTable: "User",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_User_Role_RoleId",
				table: "User",
				column: "RoleId",
				principalTable: "Role",
				principalColumn: "RoleId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_User_UserInfo_UserInfoId",
				table: "User",
				column: "UserInfoId",
				principalTable: "UserInfo",
				principalColumn: "UserInfoId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_UserAlbums_User_UserId",
				table: "UserAlbums",
				column: "UserId",
				principalTable: "User",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_UserArtists_User_UserId",
				table: "UserArtists",
				column: "UserId",
				principalTable: "User",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_UserInfo_Image_ImageId",
				table: "UserInfo",
				column: "ImageId",
				principalTable: "Image",
				principalColumn: "ImageId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_UserPlaylists_User_UserId",
				table: "UserPlaylists",
				column: "UserId",
				principalTable: "User",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_UserTracks_User_UserId",
				table: "UserTracks",
				column: "UserId",
				principalTable: "User",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Cascade);
		}
	}
}

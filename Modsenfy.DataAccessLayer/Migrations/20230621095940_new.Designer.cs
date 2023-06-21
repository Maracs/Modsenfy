﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modsenfy.DataAccessLayer.Data;

#nullable disable

namespace Modsenfy.DataAccessLayer.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230621095940_new")]
    partial class @new
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Album", b =>
                {
                    b.Property<int>("AlbumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlbumId"), 1L, 1);

                    b.Property<string>("AlbumName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AlbumOwnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AlbumRelease")
                        .HasColumnType("datetime2");

                    b.Property<int>("AlbumTypeId")
                        .HasColumnType("int");

                    b.Property<int>("CoverId")
                        .HasColumnType("int");

                    b.HasKey("AlbumId");

                    b.HasIndex("AlbumOwnerId");

                    b.HasIndex("AlbumTypeId");

                    b.HasIndex("CoverId");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.AlbumType", b =>
                {
                    b.Property<int>("AlbumTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlbumTypeId"), 1L, 1);

                    b.Property<string>("AlbumTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlbumTypeId");

                    b.ToTable("AlbumType");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistId"), 1L, 1);

                    b.Property<string>("ArtistBio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.HasKey("ArtistId");

                    b.HasIndex("ImageId");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Audio", b =>
                {
                    b.Property<int>("AudioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AudioId"), 1L, 1);

                    b.Property<string>("AudioFilename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AudioId");

                    b.ToTable("Audio");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"), 1L, 1);

                    b.Property<string>("ImageFilename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ImageTypeId")
                        .HasColumnType("int");

                    b.HasKey("ImageId");

                    b.HasIndex("ImageTypeId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.ImageType", b =>
                {
                    b.Property<int>("ImageTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageTypeId"), 1L, 1);

                    b.Property<string>("ImageTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageTypeId");

                    b.ToTable("ImageType");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Playlist", b =>
                {
                    b.Property<int>("PlaylistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlaylistId"), 1L, 1);

                    b.Property<int>("CoverId")
                        .HasColumnType("int");

                    b.Property<string>("PlaylistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlaylistOwnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PlaylistRelease")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistId");

                    b.HasIndex("CoverId");

                    b.HasIndex("UserId");

                    b.ToTable("Playlist");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.PlaylistTracks", b =>
                {
                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistId", "TrackId");

                    b.HasIndex("TrackId");

                    b.ToTable("PlaylistTracks");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"), 1L, 1);

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("RequestArtistBio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestArtistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestTimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RequestTimeProcessed")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RequestId");

                    b.HasIndex("ImageId");

                    b.HasIndex("RequestStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.RequestStatus", b =>
                {
                    b.Property<int>("RequestStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestStatusId"), 1L, 1);

                    b.Property<string>("RequestStatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestStatusId");

                    b.ToTable("RequestStatus");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Stream", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StreamDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "TrackId", "StreamDate");

                    b.HasIndex("TrackId");

                    b.ToTable("Stream");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackId"), 1L, 1);

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("AudioId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TrackDuration")
                        .HasColumnType("datetime2");

                    b.Property<string>("TrackGenius")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrackStreams")
                        .HasColumnType("int");

                    b.HasKey("TrackId");

                    b.HasIndex("AlbumId");

                    b.HasIndex("AudioId");

                    b.HasIndex("GenreId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.TrackArtists", b =>
                {
                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("ArtistId", "TrackId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackArtists");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("int");

                    b.Property<string>("UserNickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPasshash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserInfoId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.UserAlbums", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "AlbumId");

                    b.HasIndex("AlbumId");

                    b.ToTable("UserAlbums");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.UserArtists", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ArtistId");

                    b.HasIndex("ArtistId");

                    b.ToTable("UserArtists");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.UserInfo", b =>
                {
                    b.Property<int>("UserInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserInfoId"), 1L, 1);

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("UserInfoAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserInfoFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserInfoLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserInfoMiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserInfoPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserInfoRegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserInfoId");

                    b.HasIndex("ImageId");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.UserPlaylists", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<string>("UserPlaylistsAdded")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "PlaylistId");

                    b.HasIndex("PlaylistId");

                    b.ToTable("UserPlaylists");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.UserTracks", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UserTrackAdded")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "TrackId");

                    b.HasIndex("TrackId");

                    b.ToTable("UserTracks");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Album", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("AlbumOwnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.AlbumType", "AlbumType")
                        .WithMany()
                        .HasForeignKey("AlbumTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Image", "Image")
                        .WithMany()
                        .HasForeignKey("CoverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AlbumType");

                    b.Navigation("Artist");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Artist", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Image", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.ImageType", "ImageType")
                        .WithMany()
                        .HasForeignKey("ImageTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImageType");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Playlist", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Image", "Image")
                        .WithMany()
                        .HasForeignKey("CoverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.PlaylistTracks", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Playlist", "Playlist")
                        .WithMany("PlaylistTracks")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Track", "Track")
                        .WithMany("PlaylistTracks")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Request", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.RequestStatus", "RequestStatus")
                        .WithMany()
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("RequestStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Stream", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Track", "Track")
                        .WithMany("Streams")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.User", "User")
                        .WithMany("Streams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Track", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Album", "Album")
                        .WithMany("Tracks")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Audio", "Audio")
                        .WithMany()
                        .HasForeignKey("AudioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Audio");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.TrackArtists", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Artist", "Artist")
                        .WithMany("TrackArtists")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Track", "Track")
                        .WithMany("TrackArtists")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.User", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.UserAlbums", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Album", "Album")
                        .WithMany("UserAlbums")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.User", "User")
                        .WithMany("UserAlbums")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.UserArtists", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Artist", "Artist")
                        .WithMany("UserArtists")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.User", "User")
                        .WithMany("UserArtists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.UserInfo", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.UserPlaylists", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Playlist", "Playlist")
                        .WithMany("UserPlaylists")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.User", "User")
                        .WithMany("UserPlaylists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.UserTracks", b =>
                {
                    b.HasOne("Modsenfy.DataAccessLayer.Entities.Track", "Track")
                        .WithMany("UserTracks")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modsenfy.DataAccessLayer.Entities.User", "User")
                        .WithMany("UserTracks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Album", b =>
                {
                    b.Navigation("Tracks");

                    b.Navigation("UserAlbums");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Artist", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("TrackArtists");

                    b.Navigation("UserArtists");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Playlist", b =>
                {
                    b.Navigation("PlaylistTracks");

                    b.Navigation("UserPlaylists");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.Track", b =>
                {
                    b.Navigation("PlaylistTracks");

                    b.Navigation("Streams");

                    b.Navigation("TrackArtists");

                    b.Navigation("UserTracks");
                });

            modelBuilder.Entity("Modsenfy.DataAccessLayer.Entities.User", b =>
                {
                    b.Navigation("Streams");

                    b.Navigation("UserAlbums");

                    b.Navigation("UserArtists");

                    b.Navigation("UserPlaylists");

                    b.Navigation("UserTracks");
                });
#pragma warning restore 612, 618
        }
    }
}

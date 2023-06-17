using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Data;

public class DatabaseContext : DbContext
{
	public DatabaseContext(DbContextOptions<DatabaseContext> options)
		: base(options) { }

	public DbSet<Track> Tracks { get; set; }
	public DbSet<Request> Requests { get; set; } 
	public DbSet<TrackArtists> TrackArtists { get; set; }
  

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var connectionString = "Server=.\\SQLEXPRESS;Initial Catalog=Modsenfy;Trusted_Connection=True;TrustServerCertificate=True";
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer(connectionString,
				builder => builder.MigrationsAssembly("Modsenfy.DataAccessLayer"));
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<PlaylistTracks>()
			.HasKey(pt => new { pt.PlaylistId, pt.TrackId });

		modelBuilder.Entity<PlaylistTracks>()
			.HasOne(pt => pt.Playlist)
			.WithMany(p => p.PlaylistTracks)
			.HasForeignKey(pt => pt.PlaylistId);

		modelBuilder.Entity<PlaylistTracks>()
			.HasOne(pt => pt.Track)
			.WithMany(p => p.PlaylistTracks)
			.HasForeignKey(pt => pt.TrackId);

		modelBuilder.Entity<Entities.Stream>()
			.HasKey(pt => new { pt.UserId, pt.TrackId });

		modelBuilder.Entity<Entities.Stream>()
			.HasOne(pt => pt.Track)
			.WithMany(p => p.Streams)
			.HasForeignKey(pt => pt.TrackId);

		modelBuilder.Entity<Entities.Stream>()
			.HasOne(pt => pt.User)
			.WithMany(p => p.Streams)
			.HasForeignKey(pt => pt.UserId);

		modelBuilder.Entity<TrackArtists>()
			.HasKey(pt => new { pt.ArtistId, pt.TrackId });

		modelBuilder.Entity<TrackArtists>()
			.HasOne(pt => pt.Track)
			.WithMany(p => p.TrackArtists)
			.HasForeignKey(pt => pt.TrackId);

		modelBuilder.Entity<TrackArtists>()
			.HasOne(pt => pt.Artist)
			.WithMany(p => p.TrackArtists)
			.HasForeignKey(pt => pt.ArtistId);

		modelBuilder.Entity<UserAlbums>()
			.HasKey(pt => new { pt.UserId, pt.AlbumId });

		modelBuilder.Entity<UserAlbums>()
			.HasOne(pt => pt.User)
			.WithMany(p => p.UserAlbums)
			.HasForeignKey(pt => pt.UserId);

		modelBuilder.Entity<UserAlbums>()
			.HasOne(pt => pt.Album)
			.WithMany(p => p.UserAlbums)
			.HasForeignKey(pt => pt.AlbumId);

		modelBuilder.Entity<UserArtists>()
			.HasKey(pt => new { pt.UserId, pt.ArtistId });

		modelBuilder.Entity<UserArtists>()
			.HasOne(pt => pt.User)
			.WithMany(p => p.UserArtists)
			.HasForeignKey(pt => pt.UserId);

		modelBuilder.Entity<UserArtists>()
			.HasOne(pt => pt.Artist)
			.WithMany(p => p.UserArtists)
			.HasForeignKey(pt => pt.ArtistId);

		modelBuilder.Entity<UserPlaylists>()
			.HasKey(pt => new { pt.UserId, pt.PlaylistId });

		modelBuilder.Entity<UserPlaylists>()
			.HasOne(pt => pt.User)
			.WithMany(p => p.UserPlaylists)
			.HasForeignKey(pt => pt.UserId);

		modelBuilder.Entity<UserPlaylists>()
			.HasOne(pt => pt.Playlist)
			.WithMany(p => p.UserPlaylists)
			.HasForeignKey(pt => pt.PlaylistId);

		modelBuilder.Entity<UserTracks>()
			.HasKey(pt => new { pt.UserId, pt.TrackId });

		modelBuilder.Entity<UserTracks>()
			.HasOne(pt => pt.User)
			.WithMany(p => p.UserTracks)
			.HasForeignKey(pt => pt.UserId);

		modelBuilder.Entity<UserTracks>()
			.HasOne(pt => pt.Track)
			.WithMany(p => p.UserTracks)
			.HasForeignKey(pt => pt.TrackId);

		modelBuilder.Entity<Album>()
			.HasOne(a => a.Image)
			.WithMany()
			.HasForeignKey(a => a.CoverId)
			.OnDelete(DeleteBehavior.NoAction);

		modelBuilder.Entity<Playlist>()
			.HasOne(a => a.Image)
			.WithMany()
			.HasForeignKey(a => a.CoverId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}

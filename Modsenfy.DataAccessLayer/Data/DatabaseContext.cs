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

    public DbSet<Album> Albums { get; set; }

    public DbSet<Entities.Stream> Streams { get; set; }

    public DbSet<RequestStatus> RequestStatuses { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserInfo> UserInfos { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<ImageType> ImageTypes { get; set; }

    public DbSet<Artist> Artists { get; set; }

    public DbSet<Playlist> Playlists { get; set; }
    
    public DbSet<UserArtists> UserArtists { get; set; }
    
    public DbSet<UserTracks> UserTracks { get; set; }
    
    public DbSet<UserAlbums> UserAlbums { get; set; }
    
    public DbSet<Genre> Genres { get; set; }
    
    public DbSet<AlbumType> AlbumTypes { get; set; }
    
    public DbSet<Audio> Audios { get; set; }
    
    public DbSet<UserPlaylists> UserPlaylists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Data Source=34.118.70.5,1433;Initial Catalog=modsenfydb;User ID=sqlserver;Password=password;";
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly("Modsenfy.DataAccessLayer"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Playlist>()
            .HasKey(playlist => playlist.PlaylistId);

        modelBuilder.Entity<Album>()
            .HasOne(a => a.Artist)
            .WithMany()
            .HasForeignKey(a => a.AlbumOwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Artist>()
            .HasMany(a => a.Albums)
            .WithOne(a => a.Artist)
            .HasForeignKey(a => a.AlbumOwnerId)
            .OnDelete(DeleteBehavior.NoAction);

     modelBuilder.Entity<Playlist>()
            .HasOne(playlist => playlist.User)
            .WithMany(user => user.Playlists)
            .HasForeignKey(playlist => playlist.PlaylistOwnerId);

        modelBuilder.Entity<User>().HasKey(user => user.UserId);

        modelBuilder.Entity<UserInfo>().HasKey(userInfo => userInfo.UserInfoId);

        modelBuilder.Entity<UserInfo>()
            .HasOne(userInfo => userInfo.User)
            .WithOne(user => user.UserInfo)
            .HasForeignKey<User>(user => user.UserInfoId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Role>().HasKey(role => role.RoleId);

        modelBuilder.Entity<User>()
            .HasOne(user => user.Role)
            .WithMany(role => role.Users)
            .HasForeignKey(pt => pt.UserRoleId);

        modelBuilder.Entity<UserInfo>()
            .HasOne(userInfo => userInfo.Image)
            .WithMany(image => image.UserInfos)
            .HasForeignKey(pt => pt.ImageId)
            .OnDelete(DeleteBehavior.NoAction);

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
              .HasKey(pt => new { pt.UserId, pt.TrackId, pt.StreamDate });

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
            .HasForeignKey(pt => pt.UserId)
            .OnDelete(DeleteBehavior.NoAction);

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
            .HasKey(a => a.AlbumId);

        modelBuilder.Entity<Album>()
            .HasOne(a => a.Image)
            .WithMany()
            .HasForeignKey(a => a.CoverId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Album>()
            .HasOne(a => a.Artist)
            .WithMany()
            .HasForeignKey(a => a.AlbumOwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Artist>()
            .HasMany(a => a.Albums)
            .WithOne(a => a.Artist)
            .HasForeignKey(a => a.AlbumOwnerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Playlist>()
            .HasOne(a => a.Image)
            .WithMany()
            .HasForeignKey(a => a.CoverId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

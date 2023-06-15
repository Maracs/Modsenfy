using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Entities;
using Stream = Modsenfy.DataAccessLayer.Entities.Stream;

namespace Modsenfy.DataAccessLayer.Data;
public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }
        
        public DbSet<Album> Albums { get; set; }
        
        public DbSet<AlbumType> AlbumTypes { get; set; }
        
        public DbSet<Artist> Artists { get; set; }

        public DbSet<Audio> Audios { get; set; }
        
        public DbSet<Genre> Genres { get; set; }
        
        public DbSet<Image> Images { get; set; }
        
        public DbSet<ImageType> ImageTypes { get; set; }
        
        public DbSet<Playlist> Playlists { get; set; }
        
        public DbSet<PlaylistTracks> PlaylistTracks { get; set; }
        
        public DbSet<Request> Requests { get; set; }
        
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Stream> Streams{ get; set; }
        
        public DbSet<Track> Tracks { get; set; }
        
        public DbSet<TrackArtists> TrackArtists { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<UserAlbums> UserAlbums { get; set; }
        
        public DbSet<UserArtists> UserArtists { get; set; }
        
        public DbSet<UserInfo> UserInfos { get; set; }
        
        public DbSet<UserPlaylists> UserPlaylists { get; set; }
        
        public DbSet<UserTracks> UserTracks { get; set; }
    }

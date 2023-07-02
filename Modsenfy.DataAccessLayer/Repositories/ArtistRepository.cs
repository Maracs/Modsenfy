using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly DatabaseContext _databaseContext;
        
        public ArtistRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            var artist = await _databaseContext.Artists.FindAsync(id);

            return artist;
        }

        public async Task<Artist> GetByIdWithJoins(int id)
        {
            var artist = await _databaseContext.Artists
                .Include(a => a.Image)
                    .ThenInclude(i => i.ImageType)
                .Include(a => a.TrackArtists)
                    .ThenInclude(ta => ta.Track)
                        .ThenInclude(t => t.Audio)
                .Include(a => a.TrackArtists)
                    .ThenInclude(ta => ta.Track)
                        .ThenInclude(t => t.Genre)
                .Include(a => a.Albums)
                    .ThenInclude(al => al.AlbumType)
                .FirstOrDefaultAsync(a => a.ArtistId == id);

            return artist;
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _databaseContext.Artists.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task CreateAsync(Artist entity)
        {
            await _databaseContext.AddAsync(entity);
        }

        public async Task CreateWithId(Artist entity)
        {
             await _databaseContext.Database.OpenConnectionAsync();
            try
            {
               await _databaseContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Artists ON");
               await _databaseContext.AddAsync(entity);
               await _databaseContext.SaveChangesAsync();
               await _databaseContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Artists OFF");
            }
            finally
            {
                await _databaseContext.Database.CloseConnectionAsync();
            }
        }
        
        public async Task UpdateAsync(Artist entity)
        {
            var artist = await _databaseContext.Artists.FindAsync(entity.ArtistId);

            artist.ArtistName = entity.ArtistName;
            artist.ArtistBio = entity.ArtistBio;
            artist.Image = entity.Image;
            artist.ImageId = entity.ImageId;
        }

        public void DeleteAsync(Artist entity) 
        {
            _databaseContext.Remove(entity);
            _databaseContext.SaveChanges();
        }

        public async Task<IEnumerable<Artist>> GetSeveralArtists(List<int> ids)
        {
            List<Artist> artists = new List<Artist>();

            foreach (var id in ids)
            {
                artists.Add(await GetByIdWithJoins(id));
            }

            return artists;
        }

        public async Task<IEnumerable<Album>> GetArtistAlbums(int id)
        {
            var albums = await _databaseContext.Albums
                .Include(a => a.AlbumType)
                .Include(a => a.Image)
                    .ThenInclude(i => i.ImageType)
                .Where(a => a.AlbumOwnerId == id)
                .OrderByDescending(a => a.AlbumRelease)
                .ToListAsync();

            return albums;
        }

        public async Task<IEnumerable<Track>> GetArtistTracks(int id)
        {
            var tracks = await _databaseContext.Tracks
                .Include(t => t.Audio)
                .Include(t => t.Genre)
                .Where(t => t.Album.AlbumOwnerId == id)
                .OrderByDescending(t => t.Streams)
                .ToListAsync();

            return tracks;
        }

        public async Task<IEnumerable<Entities.Stream>> GetArtistStreams(int id)
        {
            var streams = await _databaseContext.Streams
                .Include(s => s.User)
                .Include(s => s.Track)
                .Where(s => s.Track.Album.AlbumOwnerId == id)
                .OrderByDescending(s => s.StreamDate)
                .ToListAsync();

            return streams;
        }
    }
}
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories
{
    public class ArtistRepository : IArtistRepository
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

        public async Task<Artist> GetByIdWithJoinsAsync(int id)
        {
            var artist = await _databaseContext.Artists
                .Include(a => a.TrackArtists)
                    .ThenInclude(ta => ta.Track)
                .Include(a => a.Albums)
                    .ThenInclude(al => al.AlbumType)
                .FirstOrDefaultAsync(a => a.ArtistId == id);

            return artist;
        }

        public async Task<IEnumerable<Artist>> GetSeveralArtistsAsync(List<int> ids)
        {
            var artists = await _databaseContext.Artists
                .Where(a => ids.Contains(a.ArtistId))
                .ToListAsync();

            return artists;
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            var artists = await _databaseContext.Artists
                .ToListAsync();

            return artists;
        }

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task CreateAsync(Artist entity)
        {
            await _databaseContext.AddAsync(entity);
        }

        public async Task UpdateAsync(Artist entity)
        {
            _databaseContext.Artists.Update(entity);
        }

        public void Delete(Artist entity)
        {
            _databaseContext.Remove(entity);
        }

        public async Task<IEnumerable<Album>> GetArtistAlbumsAsync(int id)

        {
            var albums = await _databaseContext.Albums
                .Where(a => a.AlbumOwnerId == id)
                .OrderByDescending(a => a.AlbumRelease)
                .ToListAsync();

            return albums;
        }

        public async Task<IEnumerable<Track>> GetArtistTracksAsync(int id)
        {
            var tracks = await _databaseContext.Tracks
                .Where(t => t.Album.AlbumOwnerId == id)
                .OrderByDescending(t => t.Streams)
                .ToListAsync();

            return tracks;
        }

        public async Task<IEnumerable<Entities.Stream>> GetArtistStreamsAsync(int id)
        {
            var streams = await _databaseContext.Streams
                .Where(s => s.Track.Album.AlbumOwnerId == id)
                .OrderByDescending(s => s.StreamDate)
                .ToListAsync();

            return streams;
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
    }
}

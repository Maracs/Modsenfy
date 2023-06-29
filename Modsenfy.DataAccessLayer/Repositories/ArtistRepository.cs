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

        public async Task<Artist> GetById(int id)
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

        public async Task<IEnumerable<Artist>> GetAll()
        {
            var artists = await _databaseContext.Artists
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
                .ToListAsync();

            return artists;
        }

        public async Task SaveChanges()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Create(Artist entity)
        {
            await _databaseContext.AddAsync(entity);
        }

        public async Task Update(Artist entity)
        {
            // _databaseContext.Entry(entity).State = EntityState.Modified;
            _databaseContext.Artists.Update(entity);
        }

        public void Delete(Artist entity) 
        {
            _databaseContext.Remove(entity);
            _databaseContext.SaveChanges();
        }
    }
}
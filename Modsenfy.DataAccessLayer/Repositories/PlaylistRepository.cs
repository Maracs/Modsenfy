using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PlaylistRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Playlist> GetById(int id)
        {
            var playlist = await _databaseContext.Playlists.FindAsync(id);

            return playlist;
        }

        public async Task<Playlist> GetByIdWithJoins(int id) 
        {
            var playlist = await _databaseContext.Playlists
                .Include(p => p.PlaylistTracks)
                    .ThenInclude(pt => pt.Track)
                .FirstOrDefaultAsync(p => p.PlaylistId == id);

            return playlist;
        }

        public async Task<IEnumerable<Playlist>> GetSeveralPlaylists(List<int> ids)
        {
            var playlists = await _databaseContext.Playlists
                .Where(p => ids.Contains(p.PlaylistId))
                .ToListAsync();

            return playlists;
        }

        public async Task<IEnumerable<Playlist>> GetAll()
        {
            var playlists = await _databaseContext.Playlists
                .OrderByDescending(p => p.PlaylistRelease)
                .ToListAsync();

            return playlists;
        }

        public async Task SaveChanges()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Create(Playlist entity)
        {
            await _databaseContext.AddAsync(entity);
        }

        public async Task Update(Playlist entity)
        {
            _databaseContext.Playlists.Update(entity);
        }

        public void Delete(Playlist entity) 
        {
            _databaseContext.Remove(entity);
            _databaseContext.SaveChanges();
        }
    }
}
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

        public async Task<Playlist> GetByIdAsync(int id)
        {
            var playlist = await _databaseContext.Playlists.FindAsync(id);

            return playlist;
        }

        public async Task<Playlist> GetByIdWithJoinsAsync(int id) 
        {
            var playlist = await _databaseContext.Playlists
                .Include(p => p.PlaylistTracks)
                    .ThenInclude(pt => pt.Track)
                .FirstOrDefaultAsync(p => p.PlaylistId == id);

            return playlist;
        }

        public async Task<IEnumerable<Playlist>> GetSeveralPlaylistsAsync(List<int> ids)
        {
            var playlists = await _databaseContext.Playlists
                .Where(p => ids.Contains(p.PlaylistId))
                .ToListAsync();

            return playlists;
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync()
        {
            var playlists = await _databaseContext.Playlists
                .OrderByDescending(p => p.PlaylistRelease)
                .ToListAsync();

            return playlists;
        }

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task CreateAsync(Playlist entity)
        {
            await _databaseContext.AddAsync(entity);
        }

        public async Task UpdateAsync(Playlist entity)
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
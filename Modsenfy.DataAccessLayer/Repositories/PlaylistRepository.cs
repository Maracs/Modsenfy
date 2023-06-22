using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories
{
    public class PlaylistRepository : IRepository<Playlist>
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

        public async Task<IEnumerable<Playlist>> GetAll()
        {
            return await _databaseContext.Playlists.ToListAsync();
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
            throw new NotImplementedException();
        }

        public void Delete(Playlist entity) 
        {
            _databaseContext.Remove(entity);
            _databaseContext.SaveChanges();
        }
    }
}
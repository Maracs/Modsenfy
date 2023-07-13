using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        Task<Playlist> GetByIdAsync(int id);
        Task<Playlist> GetByIdWithJoinsAsync(int id);
        Task<IEnumerable<Playlist>> GetAllAsync();
        Task SaveChangesAsync();
        Task CreateAsync(Playlist entity);
        Task UpdateAsync(Playlist entity);
        void Delete(Playlist entity);
    }
}
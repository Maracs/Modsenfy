using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        Task<Playlist> GetById(int id);
        Task<Playlist> GetByIdWithJoins(int id);
        Task<IEnumerable<Playlist>> GetAll();
        Task SaveChanges();
        Task Create(Playlist entity);
        Task Update(Playlist entity);
        void Delete(Playlist entity);
    }
}
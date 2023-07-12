using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<Artist> GetByIdAsync(int id);
        Task<Artist> GetByIdWithJoinsAsync(int id);
        Task<IEnumerable<Artist>> GetAllAsync();
        Task SaveChangesAsync();
        Task CreateAsync(Artist entity);
        Task UpdateAsync(Artist entity);
        void Delete(Artist entity);
        Task<IEnumerable<Album>> GetArtistAlbumsAsync(int id);
        Task<IEnumerable<Track>> GetArtistTracksAsync(int id);
        Task<IEnumerable<Entities.Stream>> GetArtistStreamsAsync(int id);
    }
}
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<Artist> GetById(int id);
        Task<Artist> GetByIdWithJoins(int id);
        Task<IEnumerable<Artist>> GetAll();
        Task SaveChanges();
        Task Create(Artist entity);
        Task Update(Artist entity);
        void Delete(Artist entity);
        Task<IEnumerable<Album>> GetArtistAlbums(int id);
        Task<IEnumerable<Track>> GetArtistTracks(int id);
<<<<<<< HEAD
        Task<IEnumerable<Entities.Stream>> GetArtistStreams(int id);
=======
        Task<IEnumerable<Artist>> GetArtistStreams(int id);
        
>>>>>>> dev
    }
}
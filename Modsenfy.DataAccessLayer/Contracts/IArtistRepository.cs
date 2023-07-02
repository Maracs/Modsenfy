using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts
{
    internal interface IArtistRepository : IRepository<Artist>
    {
        Task<Artist> GetById(int id);
        Task<Artist> GetByIdWithJoins(int id);
        Task<IEnumerable<Artist>> GetAll();
        Task SaveChanges();
        Task Create(Artist entity);
        Task Update(Artist entity);
        void Delete(Artist entity);
        Task<IEnumerable<Artist>> GetSeveralArtists(List<int> ids);
        Task<IEnumerable<Album>> GetArtistAlbums(int id);
        Task<IEnumerable<Track>> GetArtistTracks(int id);
        Task<IEnumerable<Artist>> GetArtistStreams(int id);
    }
}
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Modsenfy.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IAlbumRepository : IRepository<Album>
{
	IIncludableQueryable<Album, Artist> GetWithJoins();
	Task<Album> GetByIdWithJoins(int id);
	Task<IEnumerable<Album>> GetLimited(int limit, int offset);

	Task<IEnumerable<Album>> GetSkipped(int offset);
	Task<IEnumerable<Album>> GetOrderedByReleaseAndLimited(int limit, int offset);
	Task<IEnumerable<Album>> GetOrderedByRelease();
	Task<IEnumerable<Album>> GetOrderedByReleaseAndSkipped(int offset);
	Task<IEnumerable<Entities.Stream>> GetAlbumStreams(int id);
}
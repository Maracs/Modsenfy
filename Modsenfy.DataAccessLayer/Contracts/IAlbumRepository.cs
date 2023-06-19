using Microsoft.EntityFrameworkCore.ChangeTracking;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IAlbumRepository : IRepository<Album>
{
	Task<Album> GetByIdWithJoins(int id);
    IEnumerable<Album> GetLimited(int limit, int offset);

    IEnumerable<Album> GetSkipped(int offset);
    IEnumerable<Album> GetOrderedByReleaseAndLimited(int limit, int offset);
}
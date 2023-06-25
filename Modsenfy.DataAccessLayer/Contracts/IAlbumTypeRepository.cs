using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IAlbumTypeRepository : IRepository<AlbumType>
{
    Task<AlbumType> GetByName(string typeName);
}
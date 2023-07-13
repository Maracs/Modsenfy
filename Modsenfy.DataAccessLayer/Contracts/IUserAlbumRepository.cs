using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IUserAlbumRepository:IRepository<UserAlbums>
{
    async Task IRepository<UserAlbums>.SaveChanges()
    {
        throw new NotImplementedException();
    }

    async Task<UserAlbums> IRepository<UserAlbums>.GetById(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<UserAlbums>> IRepository<UserAlbums>.GetAll()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserAlbums>.Create(UserAlbums entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserAlbums>.Update(UserAlbums entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<UserAlbums>.Delete(UserAlbums entity)
    {
        throw new NotImplementedException();
    }
}
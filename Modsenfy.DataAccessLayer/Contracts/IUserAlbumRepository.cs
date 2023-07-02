using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IUserAlbumRepository:IRepository<UserAlbums>
{
    async Task IRepository<UserAlbums>.SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    async Task<UserAlbums> IRepository<UserAlbums>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<UserAlbums>> IRepository<UserAlbums>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserAlbums>.CreateAsync(UserAlbums entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserAlbums>.UpdateAsync(UserAlbums entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<UserAlbums>.DeleteAsync(UserAlbums entity)
    {
        throw new NotImplementedException();
    }
}
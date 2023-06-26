using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IUserTrackRepository:IRepository<UserTracks>
{
    async Task IRepository<UserTracks>.SaveChanges()
    {
        throw new NotImplementedException();
    }

    async Task<UserTracks> IRepository<UserTracks>.GetById(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<UserTracks>> IRepository<UserTracks>.GetAll()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserTracks>.Create(UserTracks entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserTracks>.Update(UserTracks entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<UserTracks>.Delete(UserTracks entity)
    {
        throw new NotImplementedException();
    }
}
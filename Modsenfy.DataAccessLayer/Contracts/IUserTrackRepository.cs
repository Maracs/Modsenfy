using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IUserTrackRepository:IRepository<UserTracks>
{
    async Task IRepository<UserTracks>.SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    async Task<UserTracks> IRepository<UserTracks>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<UserTracks>> IRepository<UserTracks>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserTracks>.CreateAsync(UserTracks entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserTracks>.UpdateAsync(UserTracks entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<UserTracks>.DeleteAsync(UserTracks entity)
    {
        throw new NotImplementedException();
    }
}
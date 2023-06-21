using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IUserInfoRepository:IRepository<UserInfo>
{
    async Task IRepository<UserInfo>.SaveChanges()
    {
        throw new NotImplementedException();
    }

    async Task<UserInfo> IRepository<UserInfo>.GetById(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<UserInfo>> IRepository<UserInfo>.GetAll()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserInfo>.Create(UserInfo entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserInfo>.Update(UserInfo entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<UserInfo>.Delete(UserInfo entity)
    {
        throw new NotImplementedException();
    }
    
    public async Task<UserInfo> CreateAndGet(UserInfo entity)
    {
        throw new NotImplementedException();
    }
}
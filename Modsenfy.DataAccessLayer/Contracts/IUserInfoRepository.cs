using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IUserInfoRepository:IRepository<UserInfo>
{
    async Task IRepository<UserInfo>.SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    async Task<UserInfo> IRepository<UserInfo>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<UserInfo>> IRepository<UserInfo>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserInfo>.CreateAsync(UserInfo entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<UserInfo>.UpdateAsync(UserInfo entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<UserInfo>.DeleteAsync(UserInfo entity)
    {
        throw new NotImplementedException();
    }
    
    public async Task<UserInfo> CreateAndGetAsync(UserInfo entity)
    {
        throw new NotImplementedException();
    }
}
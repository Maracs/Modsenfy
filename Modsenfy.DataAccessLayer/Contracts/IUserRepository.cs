using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IUserRepository:IRepository<User>
{

    Task<string> GetUserRole(User user);

    async Task IRepository<User>.SaveChanges()
    {
        throw new NotImplementedException();
    }

    async Task<User> IRepository<User>.GetById(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<User>> IRepository<User>.GetAll()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<User>.Create(User entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<User>.Update(User entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<User>.Delete(User entity)
    {
        throw new NotImplementedException();
    }
    
    public async Task<User> CreateAndGet(User entity)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> IfNicknameExists(string nickname)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IfEmailExists(string email)
    {
        throw new NotImplementedException();
    }
    
}
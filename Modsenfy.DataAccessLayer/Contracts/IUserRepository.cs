using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Contracts;

public interface IUserRepository:IRepository<User>
{
    Task<string> GetUserRoleAsync(User user);

    async Task IRepository<User>.SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    async Task<User> IRepository<User>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<User>> IRepository<User>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    async Task IRepository<User>.CreateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    async Task IRepository<User>.UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    void IRepository<User>.Delete(User entity)
    {
        throw new NotImplementedException();
    }
    
    public async Task<User> CreateAndGetAsync(User entity)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> IfNicknameExistsAsync(string nickname)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IfEmailExistsAsync(string email)
    {
        throw new NotImplementedException();
    }
}
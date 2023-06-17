using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories;

public class UserRepository:IUserRepository
{
    private readonly DatabaseContext _databaseContext;
    public async Task SaveChanges()
    {
         await _databaseContext.SaveChangesAsync();
        
    }

    public async Task<User> GetById(int id)
    {
        var user = await _databaseContext.Users.FindAsync(id);
        
        return user;
    }

    public async Task<bool> IfNicknameExists(string nickname)
    {
        return await _databaseContext.Users.AnyAsync(x=>x.UserNickname == nickname);
    }

    public async Task<bool> IfEmailExists(string email)
    {
        return await _databaseContext.Users.AnyAsync(x=>x.UserEmail == email);
    }
    
    public async Task<IEnumerable<User>> GetAll()
    {
        var users = await _databaseContext.Users.ToListAsync();
        
        return users;
    }

    public async Task Create(User entity)
    {
        await _databaseContext.AddAsync(entity);
    }

    public async Task<User> CreateAndGet(User entity)
    {
        var entityEntry = await _databaseContext.AddAsync(entity);
        
        return entityEntry.Entity;
    }

    public async Task Update(User entity)
    {
        _databaseContext.Update(entity);
    }

    public  void Delete(User entity)
    {
        _databaseContext.Users.Remove(entity);
    }
}
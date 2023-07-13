using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories;

public class UserInfoRepository:IUserInfoRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserInfoRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<UserInfo> CreateAndGetAsync(UserInfo entity)
    {
        var userInfoEntry= await _databaseContext.UserInfos.AddAsync(entity);

        await _databaseContext.SaveChangesAsync();
        
        return userInfoEntry.Entity;
    }

    public async Task SaveChangesAsync()
    {
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<UserInfo> GetByIdAsync(int id)
    {
        var userInfo = await _databaseContext.UserInfos.FindAsync(id);

        return userInfo;
    }

    public async Task<IEnumerable<UserInfo>> GetAllAsync()
    {
        var userInfoList = await _databaseContext.UserInfos.ToListAsync();
        
        return userInfoList;
    }

    public async Task CreateAsync(UserInfo entity)
    {
        await _databaseContext.UserInfos.AddAsync(entity);
    }

    public async Task UpdateAsync(UserInfo entity)
    {
        _databaseContext.Update(entity);
    }

    public void DeleteAsync(UserInfo entity)
    {
        _databaseContext.UserInfos.Remove(entity);
    }
}
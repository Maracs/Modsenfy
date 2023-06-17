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
    
    public async Task<UserInfo> CreateAndGet(UserInfo entity)
    {
        var userInfoEntry= await _databaseContext.UserInfos.AddAsync(entity);

        return userInfoEntry.Entity;
    }

    public async Task SaveChanges()
    {
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<UserInfo> GetById(int id)
    {
        var userInfo = await _databaseContext.UserInfos.FindAsync(id);

        return userInfo;

    }

    public async Task<IEnumerable<UserInfo>> GetAll()
    {
        var userInfoList = await _databaseContext.UserInfos.ToListAsync();
        
        return userInfoList;
    }

    public async Task Create(UserInfo entity)
    {
        await _databaseContext.UserInfos.AddAsync(entity);
    }

    public async Task Update(UserInfo entity)
    {
        _databaseContext.Update(entity);
    }

    public void Delete(UserInfo entity)
    {
        _databaseContext.UserInfos.Remove(entity);
    }
}
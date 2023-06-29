using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Modsenfy.DataAccessLayer.Repositories;

public class UserRepository:IUserRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
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

        await _databaseContext.SaveChangesAsync();
        
        return entityEntry.Entity;
    }

    public async Task Update(User entity)
    {
        _databaseContext.Users.Update(entity);
    }

    public  void Delete(User entity)
    {
        _databaseContext.Users.Remove(entity);
    }

    public async Task<User> GetByIdWithJoins(int id)
    {
       var user= await _databaseContext.Users.Include(user => user.Role)
            .Include(user => user.UserInfo)
            .ThenInclude(userInfo => userInfo.Image).ThenInclude(image => image.ImageType).FirstOrDefaultAsync(user=>user.UserId==id);
       return user;
    }

    public async Task<User> GetUserWithPlaylists(int id)
    {
        var user = await _databaseContext.Users
            .Include(user => user.Playlists)
            .ThenInclude(playlists => playlists.PlaylistTracks)
            .ThenInclude(tracks => tracks.Track)
            .ThenInclude(track => track.Audio)
            .ThenInclude(audio => audio.AudioFilename)
            .Include(user => user.Playlists)
            .ThenInclude(playlists => playlists.PlaylistTracks)
            .ThenInclude(tracks => tracks.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(artists => artists.Artist)
            .ThenInclude(artist => artist.Image)
            .ThenInclude(image => image.ImageType)
            .Include(user => user.Playlists)
            .ThenInclude(playlists => playlists.PlaylistTracks)
            .ThenInclude(tracks => tracks.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(artists => artists.Artist)
            .ThenInclude(artist => artist.UserArtists)
            .Include(user => user.Playlists)
            .ThenInclude(playlist => playlist.Image)
            .ThenInclude(image => image.ImageType)
            .Include(user => user.UserInfo)
            .ThenInclude(info => info.Image)
            .ThenInclude(image => image.ImageType)
            .FirstOrDefaultAsync(user => user.UserId == id);

        return user;
    }

    public async Task<User> GetByUsername(string username)
    {
        return await _databaseContext.Users.FirstOrDefaultAsync(x => x.UserNickname ==  username);
    }

    public async Task<string> GetUserRole(User user)
    {
        var role = await _databaseContext.Role.FindAsync(user.UserRoleId);
        if (role == null) { throw new NullReferenceException(); }
        return role.RoleName;
    }
}
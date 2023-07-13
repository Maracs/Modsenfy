using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories;

public class UserPlaylistRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserPlaylistRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task FollowPlaylistAsync(int id, int playlistId)
    {
        var entity = new UserPlaylists(){PlaylistId = playlistId,UserId = id,UserPlaylistsAdded = DateTime.Now};
        await _databaseContext.UserPlaylists.AddAsync(entity);
    }

    public async Task UnfollowPlaylistAsync(int id, int playlistId)
    {
        var entity = new UserPlaylists(){PlaylistId = playlistId,UserId = id};
        _databaseContext.UserPlaylists.Remove(entity);
    }

    public async Task<bool> IfUserFollowPlaylistAsync(int playlistId, int userId)
    {
        return await _databaseContext.UserPlaylists.AnyAsync(playlists =>
            playlists.PlaylistId == playlistId && playlists.UserId == userId);
    }

    public async Task SaveChangesAsync()
    {
        await _databaseContext.SaveChangesAsync();
    }
}
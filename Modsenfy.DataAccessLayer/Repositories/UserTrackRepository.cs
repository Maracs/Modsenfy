using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories;

public class UserTrackRepository:IUserTrackRepository
{
    private readonly DatabaseContext _databaseContext;
    
    public UserTrackRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task SaveChangesAsync()
    {
       await _databaseContext.SaveChangesAsync();
    }

    public async Task<UserTracks> GetByIdAsync(int id)
    {
       return await _databaseContext.UserTracks.FindAsync(id);
    }

    public async Task<IEnumerable<UserTracks>> GetAllAsync()
    {
        return await _databaseContext.UserTracks.ToListAsync();
    }

    public async Task CreateAsync(UserTracks entity)
    {
        await _databaseContext.UserTracks.AddAsync(entity);
    }

    public async Task UpdateAsync(UserTracks entity)
    {
        _databaseContext.UserTracks.Update(entity);
    }

    public void DeleteAsync(UserTracks entity)
    {
        _databaseContext.UserTracks.Remove(entity);
    }
    
    public async Task<bool> IfUserFollowTrackAsync(int userId,int trackId)
    {
        return (await _databaseContext.UserTracks.AnyAsync(tracks =>tracks.TrackId ==trackId && tracks.UserId == userId));
    }

    public async Task<IEnumerable<UserTracks>> GetUserTracksAsync(int id)
    {
        var userTracks = await _databaseContext.UserTracks
            .Include(userTracks => userTracks.Track)
            .ThenInclude(track => track.Audio)
            .Include(userTracks => userTracks.Track)
            .ThenInclude(track => track.Genre)
            .Include(userTracks => userTracks.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(artists => artists.Artist)
            .ThenInclude(artist => artist.Image)
            .ThenInclude(image => image.ImageType)
            .Include(userTracks => userTracks.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(artists => artists.Artist)
            .ThenInclude(artist => artist.UserArtists)
            .Include(userTracks => userTracks.Track)
            .ThenInclude(track => track.Album)
            .ThenInclude(album => album.Artist)
            .ThenInclude(artist => artist.Image)
            .ThenInclude(image => image.ImageType)
            .Include(userTracks => userTracks.Track)
            .ThenInclude(track => track.Album)
            .ThenInclude(album => album.Artist)
            .ThenInclude(artist => artist.UserArtists)
            .Include(userTracks => userTracks.Track)
            .ThenInclude(track => track.Album)
            .ThenInclude(album => album.Image)
            .ThenInclude(image => image.ImageType)
            .Include(userTracks => userTracks.Track)
            .ThenInclude(track => track.Album)
            .ThenInclude(album =>album.AlbumType )
            .Where(userTracks => userTracks.UserId == id).ToListAsync();

        return userTracks;
    }
}
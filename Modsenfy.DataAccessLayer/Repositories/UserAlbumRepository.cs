using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;

namespace Modsenfy.DataAccessLayer.Repositories;

public class UserAlbumRepository:IUserAlbumRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserAlbumRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<UserAlbums> GetByIdAsync(int userId,int albumId)
    {
        return await _databaseContext.UserAlbums.FindAsync(userId,albumId);
    }

    public async Task SaveChangesAsync()
    {
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserAlbums>> GetAllAsync()
    {
       return await _databaseContext.UserAlbums.ToListAsync();
    }

    public async Task CreateAsync(UserAlbums entity)
    {
        await _databaseContext.UserAlbums.AddAsync(entity);
    }

    public async Task UpdateAsync(UserAlbums entity)
    {
        _databaseContext.UserAlbums.Update(entity);
    }

    public void DeleteAsync(UserAlbums entity)
    {
        _databaseContext.UserAlbums.Remove(entity);
    }

    public async Task<bool> IfUserFollowAlbumAsync(int userId, int albumId)
    {
        return (await _databaseContext.UserAlbums.AnyAsync(albums   =>albums.AlbumId ==albumId && albums.UserId == userId));
    }

    public async Task<IEnumerable<UserAlbums>> GetUserSavedAlbums(int id)
    {
        var albums = await _databaseContext.UserAlbums
            .Include(userAlbums => userAlbums.Album)
                .ThenInclude(album => album.Artist)
                    .ThenInclude(artist => artist.Image)
                        .ThenInclude(image => image.ImageType)
            .Include(userAlbums => userAlbums.Album)
                .ThenInclude(album => album.Artist)
                    .ThenInclude(artist => artist.UserArtists)
            .Include(userAlbums => userAlbums.Album)
                .ThenInclude(album => album.Image)
                    .ThenInclude(image => image.ImageType)
            .Include(userAlbums => userAlbums.Album)
                .ThenInclude(album => album.Tracks)
                    .ThenInclude(track => track.Audio)
            .Include(userAlbums => userAlbums.Album)
                .ThenInclude(album => album.Tracks)
                    .ThenInclude(track => track.TrackArtists)
                        .ThenInclude(trackArtists => trackArtists.Artist)
                            .ThenInclude(artist => artist.Image)
                                .ThenInclude(image => image.ImageType)
            .Include(userAlbums => userAlbums.Album)
                .ThenInclude(album => album.Tracks)
                    .ThenInclude(track => track.TrackArtists)
                        .ThenInclude(trackArtists => trackArtists.Artist)
                            .ThenInclude(artist => artist.UserArtists)
            .Include(userAlbums => userAlbums.Album)
                .ThenInclude(album =>album.AlbumType)
            .Include(userAlbums => userAlbums.Album)
                .ThenInclude(album => album.Tracks)
                    .ThenInclude(track =>track.Genre )
            .Where(userAlbums => userAlbums.UserId == id).ToListAsync();

        return albums;
    }
}
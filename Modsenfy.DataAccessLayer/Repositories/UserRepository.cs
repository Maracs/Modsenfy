using Microsoft.EntityFrameworkCore;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Entities;
using Stream = Modsenfy.DataAccessLayer.Entities.Stream;

namespace Modsenfy.DataAccessLayer.Repositories;

public class UserRepository : IUserRepository
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
        return await _databaseContext.Users.AnyAsync(x => x.UserNickname == nickname);
    }

    public async Task<bool> IfEmailExists(string email)
    {
        return await _databaseContext.Users.AnyAsync(x => x.UserEmail == email);
    }

    public async Task<bool> IfUserFollowArtist(int userId, int artistId)
    {

        return await _databaseContext.UserArtists.AnyAsync(
            userArtists => userArtists.ArtistId == artistId && userArtists.UserId==userId);
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

    public void Delete(User entity)
    {
        _databaseContext.Users.Remove(entity);
    }

    public async Task<User> GetByIdWithJoins(int id)
    {
        var user = await _databaseContext.Users.Include(user => user.Role)
            .Include(user => user.UserInfo)
            .ThenInclude(userInfo => userInfo.Image)
            .ThenInclude(image => image.ImageType)
            .FirstOrDefaultAsync(user => user.UserId == id);
        return user;
    }

    public async Task<User> GetUserTopArtists(int id)
    {
        var user = await _databaseContext.Users
            .Include(user => user.Streams)
            .ThenInclude(stream => stream.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(trackArtists => trackArtists.Artist)
            .ThenInclude(artist => artist.Image)
            .ThenInclude(image => image.ImageType)
            .Include(user => user.Streams)
            .ThenInclude(stream => stream.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(trackArtists => trackArtists.Artist)
            .ThenInclude(artist => artist.UserArtists)
            .FirstOrDefaultAsync(user => user.UserId == id);
        return user;
    }

    public async Task<List<Stream>> GetUserTopTracks(int id)
    {
        var streams = await _databaseContext.Streams
            .Include(stream => stream.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(trackArtists => trackArtists.Artist)
            .ThenInclude(artist => artist.Image)
            .ThenInclude(image => image.ImageType)
            .Include(stream => stream.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(trackArtists => trackArtists.Artist)
            .ThenInclude(artist =>artist.UserArtists )
            .Include(stream => stream.Track)
            .ThenInclude(track => track.Album)
            .ThenInclude(album => album.Artist)
            .ThenInclude(artist => artist.UserArtists)
            .Include(stream => stream.Track)
            .ThenInclude(track => track.Album)
            .ThenInclude(album => album.Artist)
            .ThenInclude(artist => artist.Image)
            .ThenInclude(image =>image.ImageType)
            .Include(stream => stream.Track)
            .ThenInclude(track => track.Album)
            .ThenInclude(album =>album.Image)
            .ThenInclude(image =>image.ImageType)
            .Include(stream => stream.Track)
            .ThenInclude(track =>track.Audio)
            .Include(stream => stream.Track)
            .ThenInclude(track =>track.Genre)
            .Include(stream => stream.Track)
            .ThenInclude(track => track.Album)
            .ThenInclude(album =>album.AlbumType)
            .Where(stream =>  stream.UserId == id).ToListAsync();
        return streams;
    }


    public async Task<User> GetUserWithPlaylists(int id)
    {
        var user = await _databaseContext.Users
            .Include(user => user.Playlists)
            .ThenInclude(playlists => playlists.PlaylistTracks)
            .ThenInclude(tracks => tracks.Track)
            .ThenInclude(track => track.Audio)
            .Include(user => user.Playlists)
            .ThenInclude(playlists => playlists.PlaylistTracks)
            .ThenInclude(tracks => tracks.Track)
            .ThenInclude(track =>track.Genre )
            .Include(user => user.Playlists)
            .ThenInclude(playlists => playlists.UserPlaylists)
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
    
    public async Task<List<UserPlaylists>> GetUserWithSavedPlaylists(int id)
    {
        var userPlaylistsList = await _databaseContext.UserPlaylists
            .Include(playlists =>playlists.Playlist )
            .ThenInclude(playlists => playlists.PlaylistTracks)
            .ThenInclude(tracks => tracks.Track)
            .ThenInclude(track => track.Audio)
            .Include(playlists =>playlists.Playlist )
            .ThenInclude(playlists => playlists.PlaylistTracks)
            .ThenInclude(tracks => tracks.Track)
            .ThenInclude(track =>track.Genre )
            .Include(playlists =>playlists.Playlist )
            .ThenInclude(playlists => playlists.UserPlaylists)
            .Include(playlists =>playlists.Playlist )
            .ThenInclude(playlists => playlists.PlaylistTracks)
            .ThenInclude(tracks => tracks.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(artists => artists.Artist)
            .ThenInclude(artist => artist.Image)
            .ThenInclude(image => image.ImageType)
            .Include(playlists =>playlists.Playlist )
            .ThenInclude(playlists => playlists.PlaylistTracks)
            .ThenInclude(tracks => tracks.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(artists => artists.Artist)
            .ThenInclude(artist => artist.UserArtists)
            .Include(playlists =>playlists.Playlist )
            .ThenInclude(playlist => playlist.Image)
            .ThenInclude(image => image.ImageType)
            .Include(playlists =>playlists.Playlist )
            .ThenInclude(playlist =>playlist.User)
            .ThenInclude(user1 =>user1.UserInfo )
            .ThenInclude(info => info.Image)
            .ThenInclude(image => image.ImageType)
            .Where(user => user.UserId == id).ToListAsync();

        return userPlaylistsList;
    }

    public async Task FollowArtist(UserArtists entity)
    {
        await _databaseContext.UserArtists.AddAsync(entity);
    }

    public async Task UnfollowArtist(UserArtists entity)
    {
        _databaseContext.UserArtists.Remove(entity);
    }

    public async Task<User> GetUserRequests(int id)
    {
        var user = await _databaseContext.Users
            .Include(user => user.Requests)
            .ThenInclude(request => request.RequestStatus)
            .Include(user => user.Requests)
            .ThenInclude(request => request.Image)
            .ThenInclude(image => image.ImageType)
            .Include(user =>user.UserInfo )
            .ThenInclude(info =>info.Image )
            .ThenInclude(image =>image.ImageType )
            .FirstOrDefaultAsync(user => user.UserId == id);
        return user;
    }
    
    public async Task<User> GetFollowedArtists(int id)
    {
        var user = await _databaseContext.Users
            .Include(user => user.UserArtists)
            .ThenInclude(userArtists => userArtists.Artist)
            .ThenInclude(artist => artist.Image)
            .ThenInclude(image => image.ImageType)
            .Include(user => user.UserArtists)
            .ThenInclude(userArtists => userArtists.Artist)
            .ThenInclude(artist => artist.UserArtists)
            .FirstOrDefaultAsync(user => user.UserId == id);
        return user;
    }

    public async Task<ICollection<Stream>> GetUserStreamHistory(int id)
    {
        var streams = await _databaseContext.Streams
            .Include(stream => stream.User)
            .ThenInclude(user => user.UserInfo)
            .ThenInclude(info => info.Image)
            .ThenInclude(image => image.ImageType)
            .Include(stream => stream.Track)
            .ThenInclude(track => track.Genre)
            .Include(stream => stream.Track)
            .ThenInclude(track => track.Audio)
            .Include(stream => stream.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(artists => artists.Artist)
            .ThenInclude(artist => artist.Image)
            .ThenInclude(image => image.ImageType)
            .Include(stream => stream.Track)
            .ThenInclude(track => track.TrackArtists)
            .ThenInclude(artists => artists.Artist)
            .ThenInclude(artist => artist.UserArtists)
            .Where(stream => stream.UserId == id).ToListAsync();

        return streams;


    }


}
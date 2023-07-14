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

    public async Task SaveChangesAsync()
    {
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _databaseContext.Users.FindAsync(id);

        return user;
    }

    public async Task<bool> IfNicknameExistsAsync(string nickname)
    {
        return await _databaseContext.Users.AnyAsync(x => x.UserNickname == nickname);
    }

    public async Task<bool> IfEmailExistsAsync(string email)
    {
        return await _databaseContext.Users.AnyAsync(x => x.UserEmail == email);
    }

    public async Task<bool> IfUserFollowArtistAsync(int userId, int artistId)
    {
        return await _databaseContext.UserArtists.AnyAsync(
            userArtists => userArtists.ArtistId == artistId && userArtists.UserId==userId);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _databaseContext.Users.ToListAsync();

        return users;
    }

    public async Task CreateAsync(User entity)
    {
        await _databaseContext.AddAsync(entity);
    }

    public async Task<User> CreateAndGetAsync(User entity)
    {
        var entityEntry = await _databaseContext.AddAsync(entity);

        await _databaseContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task UpdateAsync(User entity)
    {
        _databaseContext.Users.Update(entity);
    }

    public void DeleteAsync(User entity)
    {
        _databaseContext.Users.Remove(entity);
    }

    public async Task<User> GetByIdWithJoinsAsync(int id)
    {
        var user = await _databaseContext.Users.Include(user => user.Role)
            .Include(user => user.UserInfo)
            .ThenInclude(userInfo => userInfo.Image)
            .ThenInclude(image => image.ImageType)
            .FirstOrDefaultAsync(user => user.UserId == id);
        
        return user;
    }

    public async Task<User> GetUserTopArtistsAsync(int id)
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

    public async Task<List<Stream>> GetUserTopTracksAsync(int id)
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

    public async Task<User> GetUserWithPlaylistsAsync(int id)
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
    
    public async Task<List<UserPlaylists>> GetUserWithSavedPlaylistsAsync(int id)
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

    public async Task FollowArtistAsync(UserArtists entity)
    {
        await _databaseContext.UserArtists.AddAsync(entity);
    }

    public async Task UnfollowArtistAsync(UserArtists entity)
    {
        _databaseContext.UserArtists.Remove(entity);
    }

    public async Task<User> GetUserRequestsAsync(int id)
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
    
    public async Task<User> GetFollowedArtistsAsync(int id)
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

    public async Task<ICollection<Stream>> GetUserStreamHistoryAsync(int id)
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

    public async Task<Playlist> CreateAndGetPlaylistAsync(Playlist entity)
    {
        var entityEntry = await _databaseContext.Playlists.AddAsync(entity);

        await _databaseContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<User> GetByUsername(string username)
    {
        return await _databaseContext.Users.FirstOrDefaultAsync(x => x.UserNickname ==  username);
    }

    public async Task<string> GetUserRoleAsync(User user)
    {
        var role = await _databaseContext.Role.FindAsync(user.UserRoleId);
        if (role == null) { throw new NullReferenceException(); }
        
        return role.RoleName;
    }
    
    public async Task<string> GetUserRoleByIdAsync(int userId)
    {
        var user = await _databaseContext.Users.FindAsync(userId);
        var role = await _databaseContext.Role.FindAsync(user.UserRoleId);
        if (role == null) { throw new NullReferenceException(); }
        
        return role.RoleName;
    }
}
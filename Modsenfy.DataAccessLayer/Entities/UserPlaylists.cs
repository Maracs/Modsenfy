namespace Modsenfy.DataAccessLayer.Entities;

public class UserPlaylists
{
    public int UserId { get; set; }

    public User User { get; set; }

    public int PlaylistId { get; set; }

    public Playlist Playlist { get; set; }

    public string UserPlaylistsAdded { get; set; }
}
using System.Collections.Generic;

namespace Modsenfy.DataAccessLayer.Entities;

public class User
{
    public int UserId { get; set; }

    public string UserNickname { get; set; }

    public string UserEmail { get; set; }

    public string UserPasshash { get; set; }

    public string UserPasshashSalt { get; set; }

    public int UserRoleId { get; set; }

    public Role Role { get; set; }

    public int UserInfoId { get; set; }

    public UserInfo UserInfo { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<UserArtists> UserArtists { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<UserTracks> UserTracks { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<Stream> Streams { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<UserAlbums> UserAlbums { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<UserPlaylists> UserPlaylists { get; set; }
    
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<Playlist> Playlists { get; set; }
}
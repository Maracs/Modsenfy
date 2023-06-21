using System;
using System.Collections.Generic;

namespace Modsenfy.DataAccessLayer.Entities;

public class Playlist
{
    public int PlaylistId { get; set; }

    public string PlaylistName { get; set; }

    public DateTime PlaylistRelease { get; set; }

    public int CoverId { get; set; }

    public Image Image { get; set; }

    public int PlaylistOwnerId { get; set; }

    public User User { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<UserPlaylists> UserPlaylists { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<PlaylistTracks> PlaylistTracks { get; set; }
}
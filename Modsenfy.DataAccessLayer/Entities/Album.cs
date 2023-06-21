using System;
using System.Collections.Generic;

namespace Modsenfy.DataAccessLayer.Entities;

public class Album
{
    public int AlbumId { get; set; }

    public string AlbumName { get; set; }

    public DateTime AlbumRelease { get; set; }

    public int AlbumOwnerId { get; set; }

    public Artist Artist { get; set; }

    public int CoverId { get; set; }

    public Image Image { get; set; }

    public int AlbumTypeId { get; set; }

    public AlbumType AlbumType { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<Track> Tracks { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<UserAlbums> UserAlbums { get; set; }
}
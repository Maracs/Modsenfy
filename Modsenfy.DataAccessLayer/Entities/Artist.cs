using System.Collections.Generic;

namespace Modsenfy.DataAccessLayer.Entities;

public class Artist
{
    public int ArtistId { get; set; }

    public string ArtistName { get; set; }

    public string ArtistBio { get; set;}

    public int ImageId { get; set; }

    public Image Image { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<Album> Albums { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<TrackArtists> TrackArtists { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<UserArtists> UserArtists { get; set; }
}
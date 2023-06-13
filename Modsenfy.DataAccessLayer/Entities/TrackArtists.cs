namespace Modsenfy.DataAccessLayer.Entities;

public class TrackArtists
{
    public int TrackId { get; set; }

    public Track Track { get; set; }

    public int ArtistId { get; set; }

    public Artist Artist { get; set; }
}
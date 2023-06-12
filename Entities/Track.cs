namespace Modsenfy.Entities
{
    public class Track
    {
        public int TrackId { get; set; }

        public string TrackName { get; set; }

        public int TrackStreams { get; set; }

        public TimeOnly TrackDuration { get; set; }

        public string TrackGenius { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set;}
        
        public int AudioId { get; set; }

        public Audio Audio { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<TrackArtists> TrackArtists { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<UserTracks> UserTracks { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Stream> Streams { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<PlaylistTracks> PlaylistTracks { get; set; }
    }
}

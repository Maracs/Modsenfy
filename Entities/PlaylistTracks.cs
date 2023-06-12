namespace Modsenfy.Entities
{
    public class PlaylistTracks
    {
        public int PlaylistId { get; set; }

        public Playlist Playlist { get; set; }

        public int TrackId { get; set; }

        public Track Track { get; set; }
    }
}

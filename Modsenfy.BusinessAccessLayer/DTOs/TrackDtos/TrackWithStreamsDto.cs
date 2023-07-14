namespace Modsenfy.BusinessAccessLayer.DTOs;

public class TrackWithStreamsDto
{
    public int TrackId { get; set; }
    public string TrackName { get; set; }
    public int TrackStreams { get; set; }
    public string TrackGenre { get; set; }
    public string TrackGenius { get; set; }
    public AudioDto Audio { get; set; }
    public IEnumerable<ArtistDto> Artists { get; set; }
    public IEnumerable<InnerStreamWithListenerDto> StreamsListeners { get; set; }
}

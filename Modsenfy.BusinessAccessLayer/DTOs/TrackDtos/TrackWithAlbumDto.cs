namespace Modsenfy.BusinessAccessLayer.DTOs;

public class TrackWithAlbumDto
{
	public int TrackId { get; set; }
	public string TrackName { get; set; }
	public int TrackStreams { get; set; }
	public string TrackGenre { get; set; }
	public string TrackGenius { get; set; }
	public DateTime TrackDuration { get; set; } 
	public IEnumerable<ArtistDto> Artists { get; set; }
	public AudioDto Audio { get; set; }
	public AlbumDto Album { get; set; }
}
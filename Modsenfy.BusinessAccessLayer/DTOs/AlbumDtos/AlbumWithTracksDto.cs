namespace Modsenfy.BusinessAccessLayer.DTOs;

public class AlbumWithTracksDto
{
	public int AlbumId { get; set; }
	public string AlbumName { get; set; }
	public DateOnly AlbumRelease { get; set; }
	public string AlbumType { get; set; }
	public int AlbumStreams { get; set; }
	public ImageDto Image { get; set; }
	public ArtistDto Artist { get; set; }

    public IEnumerable<TrackDto> Tracks{ get; set; }
}
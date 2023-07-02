namespace Modsenfy.BusinessAccessLayer.DTOs;

public class SearchDto
{
	public IEnumerable<AlbumDto> Albums { get; set; }
	public IEnumerable<TrackDto> Tracks { get; set; }
	public IEnumerable<ArtistDto> Artists { get; set; }
	public IEnumerable<PlaylistDto> Playlists { get; set; }
}
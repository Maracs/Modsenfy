namespace Modsenfy.BusinessAccessLayer.DTOs;

public class AlbumUpdateDto
{
	public string AlbumName { get; set; }
	public string AlbumTypeName { get; set; }
	public ImageDto Image { get; set; }
	public IEnumerable<TrackCreateDto> AddTracks { get; set; }
	public IEnumerable<int> DeleteTracks { get; set; }
}
namespace Modsenfy.BusinessAccessLayer.DTOs;

public class AlbumCreateDto
{
	public string Name { get; set; }
	public IEnumerable<TrackCreateDto> Tracks { get; set; }
	public ImageDto Image { get; set; }

}
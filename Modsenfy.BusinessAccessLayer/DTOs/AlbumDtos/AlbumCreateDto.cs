namespace Modsenfy.BusinessAccessLayer.DTOs;

public class AlbumCreateDto
{
	public string AlbumName { get; set; }
	public IEnumerable<TrackCreateDto> Tracks { get; set; }
	public string AlbumTypeName { get; set; }
    public ImageDto Image { get; set; }

}
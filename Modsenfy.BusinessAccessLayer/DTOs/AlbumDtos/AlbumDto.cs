namespace Modsenfy.BusinessAccessLayer.DTOs;

public class AlbumDto
{
	public int AlbumId { get; set; }
  
  public string AlbumName { get; set; }
  
	public DateOnly AlbumRelease { get; set; }

  public ArtistDto Artist { get; set; }
  
	public string AlbumTypeName { get; set; }

	public int AlbumStreams { get; set; }

	public ImageDto Image { get; set; }
}
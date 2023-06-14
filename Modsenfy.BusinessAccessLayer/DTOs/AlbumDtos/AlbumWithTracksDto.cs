namespace Modsenfy.BusinessAccessLayer.DTOs;

public class AlbumWithTracksDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public DateOnly Release { get; set; }
	public int Type { get; set; }
	public int Streams { get; set; }
	public ImageDto Image { get; set; }
	
	public IEnumerable<TrackDto> Tracks{ get; set; }
}
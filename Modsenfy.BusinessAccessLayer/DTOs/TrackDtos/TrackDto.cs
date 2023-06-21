namespace Modsenfy.BusinessAccessLayer.DTOs;

public class TrackDto
{
	public int Id { get; set; }
	
	public string Name { get; set; }
	
	public int Streams { get; set; }
	
	public string Genre { get; set; }

	public string Duration { get; set; }

	public string Genius { get; set; }
	
	public IEnumerable<ArtistDto> Artists { get; set; }
	
	public AudioDto Audio { get; set; }
}
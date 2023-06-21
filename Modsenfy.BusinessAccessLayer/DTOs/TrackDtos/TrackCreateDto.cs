namespace Modsenfy.BusinessAccessLayer.DTOs;

public class TrackCreateDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Genre { get; set; }
	public string Genius { get; set; }
	public string Duration { get; set; }
    public IEnumerable<int> Artists { get; set; }
	public AudioDto Audio { get; set; }
}
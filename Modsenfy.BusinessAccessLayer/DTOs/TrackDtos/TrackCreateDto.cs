namespace Modsenfy.BusinessAccessLayer.DTOs;

public class TrackCreateDto
{
	public string TrackName { get; set; }
	public string GenreName { get; set; }
	public string TrackGenius { get; set; }
	public string TrackDuration { get; set; }
    public IEnumerable<int> Artists { get; set; }
	public AudioDto Audio { get; set; }
}
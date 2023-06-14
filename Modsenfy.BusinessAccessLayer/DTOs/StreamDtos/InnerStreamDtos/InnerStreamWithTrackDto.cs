namespace Modsenfy.BusinessAccessLayer.DTOs;

public class InnerStreamWithTrackDto
{
	public int Id { get; set; }
    public string Date { get; set; }
	public UserDto Listener { get; set; }
	public TrackDto Track { get; set; }
}
namespace Modsenfy.BusinessAccessLayer.DTOs;

//used for other entities (like Album, Playlist, Artist) in collection
public class StreamDto
{	
	public int Id { get; set; }
	public string Date { get; set; }
	public UserDto Listener { get; set; }
	public TrackDto Track { get; set; }

}
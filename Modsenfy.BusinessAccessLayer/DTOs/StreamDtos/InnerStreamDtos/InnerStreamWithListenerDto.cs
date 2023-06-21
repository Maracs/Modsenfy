namespace Modsenfy.BusinessAccessLayer.DTOs;

public class InnerStreamWithListenerDto
{
    public int TrackId { get; set; }
	public string StreamDate { get; set; }
	public UserDto Listener { get; set; }
}
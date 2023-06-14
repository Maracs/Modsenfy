namespace Modsenfy.BusinessAccessLayer.DTOs;

public class InnerStreamWithListenerDto
{
    public int Id { get; set; }
	public string Date { get; set; }
	public UserDto Listener { get; set; }
}
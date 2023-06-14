namespace Modsenfy.BusinessAccessLayer.DTOs;

public class UserWithDetailsAndEmailDto
{
	public string Nickname { get; set; }
	public string Email { get; set; }
    public ImageDto Image { get; set; }
	public UserDetailsDto Details { get; set; }
}
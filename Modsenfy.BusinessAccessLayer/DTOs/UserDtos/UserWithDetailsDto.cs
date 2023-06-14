namespace Modsenfy.BusinessAccessLayer.DTOs;

public class UserWithDetailsDto
{
	public int Id { get; set; }
	public string Nickname { get; set; }
	public ImageDto Image { get; set; }
	public UserDetailsDto Details { get; set; }
}
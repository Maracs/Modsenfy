namespace Modsenfy.BusinessAccessLayer.DTOs;

public class UserWithIdAndDetailsAndEmailDto
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public ImageDto Image { get; set; }
    public UserDetailsDto Details { get; set; }
}
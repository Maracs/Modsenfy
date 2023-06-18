namespace Modsenfy.BusinessAccessLayer.DTOs;

public class UserWithDetailsAndEmailAndIdAndRoleDto
{
    public string Nickname { get; set; }
    public int Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public ImageDto Image { get; set; }
    public UserDetailsDto Details { get; set; }
}
namespace Modsenfy.BusinessAccessLayer.DTOs;

public class ArtistDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Bio { get; set; }
	public ImageDto Image { get; set; }
	public ArtistFollowersDto Followers { get; set; }
}
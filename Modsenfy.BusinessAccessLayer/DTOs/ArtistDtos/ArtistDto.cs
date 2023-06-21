namespace Modsenfy.BusinessAccessLayer.DTOs;

public class ArtistDto
{
	public int ArtistId { get; set; }

	public string ArtistName { get; set; }

	public string ArtistBio { get; set; }

	public ImageDto Image { get; set; }

	public ArtistFollowersDto Followers { get; set; }
}
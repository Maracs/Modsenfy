namespace Modsenfy.BusinessAccessLayer.DTOs;

public class PlaylistDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Release { get; set; }
	public UserDto Owner { get; set; }
	public PlaylistFollowersDto Followers { get; set; }
    public IEnumerable<TrackDto> Tracks { get; set; }
    
	public ImageDto Image { get; set; }

}
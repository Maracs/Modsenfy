namespace Modsenfy.BusinessAccessLayer.DTOs;

public class PlaylistFollowersDto
{
    private string url;
    public int Total { get; set; }
	public string Url 
	{
        get { return url; }
        set { url = $"https://modsenfy.by/playlists/{value}/followers"; }
    }
}
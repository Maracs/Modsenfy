namespace Modsenfy.BusinessAccessLayer.DTOs;

public class ArtistFollowersDto
{
    private string url;
    public int Total { get; set; }
	public string Url 
	{
        get { return url; }
        set { url = $"https://modsenfy.by/artists/{value}/followers"; }
    }
}
namespace Modsenfy.DataAccessLayer.Entities;

public class Audio
{
	public int AudioId { get; set; }
	public string AudioFilename { get; set; }
	
	[Newtonsoft.Json.JsonIgnore]
	[System.Text.Json.Serialization.JsonIgnore]
	public Track Track { get; set; }
}
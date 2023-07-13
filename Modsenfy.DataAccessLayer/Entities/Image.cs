namespace Modsenfy.DataAccessLayer.Entities;

public class Image
{
    public int ImageId { get; set; }
    public string ImageFilename { get; set; }
    public int ImageTypeId { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<UserInfo> UserInfos { get; set; }
    public ImageType ImageType { get; set; }
}
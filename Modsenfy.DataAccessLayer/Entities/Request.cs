namespace Modsenfy.DataAccessLayer.Entities;

public class Request
{
    public int RequestId { get; set; }

    public string RequestArtistName { get; set; }

    public string RequestArtistBio { get; set; }

    public DateTime RequestTimeCreated { get; set; }

    public DateTime RequestTimeProcessed { get; set; }

    public int RequestStatusId { get; set; }

    public RequestStatus RequestStatus { get; set; }

    public int ImageId { get; set; }

    public Image Image { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }
}
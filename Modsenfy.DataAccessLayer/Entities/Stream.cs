namespace Modsenfy.DataAccessLayer.Entities;

public class Stream
{
    public DateTime StreamDate { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public int TrackId { get; set; }

    public Track Track { get; set; }
}
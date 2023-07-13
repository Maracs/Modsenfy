using System;

namespace Modsenfy.DataAccessLayer.Entities;

public class UserTracks
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int TrackId { get; set; }
    public Track Track { get; set; }
    public DateTime UserTrackAdded { get; set; }
}
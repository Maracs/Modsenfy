using System;

namespace Modsenfy.DataAccessLayer.Entities;

public class Request
{
    public int RequestId { get; set; }

    public string RequestArtistName { get; set; }

    public string RequestArtistBio { get; set; }

    public DateTime RequestTime { get; set; }

    public int RequestStatusId { get; set; }
}
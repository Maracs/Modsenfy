namespace Modsenfy.BusinessAccessLayer.DTOs;
//Used for track as single object
public class TrackStreamDto
{
    public TrackDto Track { get; set; }
    public IEnumerable<InnerStreamWithListenerDto> Streams { get; set; }
}
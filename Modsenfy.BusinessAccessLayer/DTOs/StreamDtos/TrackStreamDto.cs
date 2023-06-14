namespace Modsenfy.BusinessAccessLayer.DTOs;

public class TrackStreamDto
{
    public TrackDto Track { get; set; }
    public IEnumerable<InnerStreamWithListenerDto> Streams { get; set; }
}
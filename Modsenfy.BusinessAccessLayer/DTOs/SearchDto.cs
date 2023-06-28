using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modsenfy.BusinessAccessLayer.DTOs;

public class SearchDto
{
    public IEnumerable<AlbumDto> Albums { get; set; }
    public IEnumerable<TrackDto> Tracks { get; set; }
    public IEnumerable<ArtistDto> Artists { get; set; }
}

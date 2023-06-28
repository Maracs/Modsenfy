using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Repositories;
using Modsenfy.BusinessAccessLayer.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Modsenfy.BusinessAccessLayer.Services;

public class ArtistService
{
    private readonly ArtistRepository _artistRepository;
    private readonly IMapper _mapper;
    private readonly DatabaseContext _databaseContext;

    public ArtistService(ArtistRepository artistRepository, IMapper mapper, DatabaseContext databaseContext)
    {
        _artistRepository = artistRepository;
		_mapper = mapper;
        _databaseContext = databaseContext;
    }

    public async Task<IEnumerable<ArtistDto>> GetSeveralArtists(List<int> ids)
    {
        IEnumerable<Artist> artists = new List<Artist>();

        foreach (var id in ids)
        {
            var artist = await _artistRepository.GetByIdWithJoins(id);
            artists.Append(artist);
        }

        IEnumerable<ArtistDto> artistDtos = _mapper.Map<IEnumerable<ArtistDto>>(artists);
        
        return artistDtos;
    }

    public async Task<IEnumerable<AlbumDto>> GetArtistAlbums(int id)
    {
        var albums = await _databaseContext.Albums
            .Include(a => a.AlbumType)
            .Include(a => a.Image)
                .ThenInclude(i => i.ImageType)
            .Where(a => a.AlbumOwnerId == id)
            .OrderByDescending(a => a.AlbumRelease)
            .ToListAsync();

        IEnumerable<AlbumDto> albumDtos = _mapper.Map<IEnumerable<AlbumDto>>(albums);

        return albumDtos;
    }

    public async Task<IEnumerable<TrackDto>> GetArtistTracks(int id)
    {
        var tracks = await _databaseContext.Tracks
            .Include(t => t.Audio)
            .Include(t => t.Genre)
            .Where(t => t.Album.AlbumOwnerId == id)
            .OrderByDescending(t => t.Streams)
            .ToListAsync();

        IEnumerable<TrackDto> trackDtos = _mapper.Map<IEnumerable<TrackDto>>(tracks);

        return trackDtos;
    }

    public async Task<IEnumerable<StreamDto>> GetArtistStreams(int id)
    {
        var streams = await _databaseContext.Streams
            .Include(s => s.User)
            .Include(s => s.Track)
            .Where(s => s.Track.Album.AlbumOwnerId == id)
            .OrderByDescending(s => s.StreamDate)
            .ToListAsync();

        IEnumerable<StreamDto> streamDtos = _mapper.Map<IEnumerable<StreamDto>>(streams);

        return streamDtos;
    }
}
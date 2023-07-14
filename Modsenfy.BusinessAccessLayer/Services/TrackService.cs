using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.DataAccessLayer.Contracts;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.BusinessAccessLayer.Services;

public class TrackService
{
    private readonly TrackRepository _trackRepository;
    private readonly GenreRepository _genreRepository;
    private readonly ArtistRepository _artistRepository;
    private readonly AudioRepository _audioRepository;
    private readonly TrackArtistsRepository _trackArtistsRepository;
    private readonly IMapper _mapper;

    public TrackService(
        TrackRepository trackRepository,
        GenreRepository genreRepository,
        ArtistRepository artistRepository,
        TrackArtistsRepository trackArtistsRepository,
        AudioRepository audioRepository,
        IMapper mapper)
    {
        _trackRepository = trackRepository;
        _genreRepository = genreRepository;
        _artistRepository = artistRepository;
        _trackArtistsRepository = trackArtistsRepository;
        _audioRepository = audioRepository;
        _mapper = mapper;
    }

    public async Task CreateTrackAsync(TrackCreateDto trackDto, int albumId, int artistOwnerId)
    {
        var genre = await _genreRepository.GetByName(trackDto.GenreName);
        var audio = new Audio()
        {
            AudioFilename = trackDto.Audio.AudioFilename
        };

        var addedAudio = await _audioRepository.CreateAndGetAsync(audio);

        var track = new Track()
        {
            TrackName = trackDto.TrackName,
            TrackStreams = 0,
            TrackDuration = DateTime.Parse("0:" + trackDto.TrackDuration),
            TrackGenius = trackDto.TrackGenius,
            AudioId = addedAudio.AudioId,
            GenreId = genre.GenreId,
            AlbumId = albumId
        };

        var addedTrack = await _trackRepository.CreateAndGetAsync(track);
        var trackArtist = new TrackArtists
        {
            TrackId = addedTrack.TrackId,
            ArtistId = artistOwnerId
        };

        await _trackArtistsRepository.CreateAsync(trackArtist);

        foreach (var artistId in trackDto.Artists)
        {
            var trackArtists = new TrackArtists
            {
                TrackId = addedTrack.TrackId,
                ArtistId = artistId
            };

            await _trackArtistsRepository.CreateAsync(trackArtists);
        }
    }

    public async Task<Track> GetTrack(int id)
    {
        return await _trackRepository.GetByIdWithJoinsAsync(id);
    }

    public async Task<Track> UpdateTrackAsync(int userId, TrackDto trackDto)
    {
        if (!(await _trackRepository.IsTrackOwnerAsync(userId, trackDto.TrackId)))
            return null;
        var genre = await _genreRepository.GetByName(trackDto.GenreName);
        var trackUpdate = await _trackRepository.GetByIdAsync(trackDto.TrackId);
        var audio = await _audioRepository.GetByIdAsync(trackUpdate.AudioId);
        trackUpdate.TrackDuration = trackDto.TrackDuration;
        trackUpdate.TrackGenius = trackDto.TrackGenius;
        audio.AudioFilename = trackDto.Audio.AudioFilename;
        trackUpdate.TrackName = trackDto.TrackName;
        trackUpdate.GenreId = genre.GenreId;

        await _trackRepository.UpdateAsync(trackUpdate);
        await _audioRepository.UpdateAsync(audio);
        return trackUpdate;
    }

    public async Task<Track> DeleteTrackAsync(int userId, int id)
    {
        if (!(await _trackRepository.IsTrackOwnerAsync(userId, id)))
            return null;

        var track = await _trackRepository.GetByIdAsync(id);
        var audio = await _audioRepository.GetByIdAsync(track.AudioId);
        _trackRepository.Delete(track);
        _audioRepository.Delete(audio);
        await _trackRepository.SaveChangesAsync();

        return track;
    }

    public async Task<TrackWithStreamsDto> GetTrackStreamsAsync(int userId, int id)
    {
        if (!(await _trackRepository.IsTrackOwnerAsync(userId, id)))
            return null;

        var track = await _trackRepository.GetByIdWithStreamsAsync(id);
        var trackDto = _mapper.Map<TrackWithStreamsDto>(track);

        return trackDto;
    }

    public async Task<IEnumerable<TrackWithAlbumDto>> GetSeverlTracksAsync(int limit, int offset, string ids)
    {
        IEnumerable<Track> tracks;
        IEnumerable<int> intIds;
        
        if (ids.Equals("all"))
        {
            if (limit == -1 && offset == 0)
                tracks = await _trackRepository.GetAllAsync();
            else if (limit == -1)
                tracks = await _trackRepository.GetSkippedAsync(offset);
            else
                tracks = await _trackRepository.GetLimitedAsync(limit, offset);
        }
        else
        {
            var splittedIds = ids.Split(',');
            intIds = splittedIds.Select(id => int.Parse(id));
            tracks = new List<Track>();
            tracks = await _trackRepository.GetByListWithJoinsAsync(intIds);
        }

        IEnumerable<TrackWithAlbumDto> trackDtos = new List<TrackWithAlbumDto>();
        
        foreach (var track in tracks)
        {
            var trackDto = _mapper.Map<TrackWithAlbumDto>(track);
            trackDtos = trackDtos.Append(trackDto);
        }
        
        return trackDtos;
    }
}
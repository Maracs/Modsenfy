using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Data;
using Modsenfy.DataAccessLayer.Repositories;
using Modsenfy.BusinessAccessLayer.DTOs;
using AutoMapper;

namespace Modsenfy.BusinessAccessLayer.Services;

public class PlaylistService
{
    private readonly PlaylistRepository _playlistRepository;
    private readonly IMapper _mapper;
    private readonly DatabaseContext _databaseContext;

    public PlaylistService(PlaylistRepository playlistRepository, IMapper mapper, DatabaseContext databaseContext)
    {
        _playlistRepository = playlistRepository;
		_mapper = mapper;
        _databaseContext = databaseContext;
    }

    public async Task<IEnumerable<PlaylistDto>> GetSeveralPlaylists(List<int> ids)
    {
        IEnumerable<Playlist> playlists = new List<Playlist>();

        foreach (var id in ids)
        {
            var playlist = await _playlistRepository.GetByIdWithJoins(id);
            playlists.Append(playlist);
        }

        IEnumerable<PlaylistDto> playlistDtos = _mapper.Map<IEnumerable<PlaylistDto>>(playlists);

        return playlistDtos;
    }

    public async Task<IEnumerable<TrackDto>> GetTracksOfPlaylist(int id)
    {
        var playlist = await _playlistRepository.GetByIdWithJoins(id);
        var tracks = playlist.PlaylistTracks;

        IEnumerable<TrackDto> trackDtos = _mapper.Map<IEnumerable<TrackDto>>(tracks);

        return trackDtos;
    }

    // public async Task<IEnumerable<StreamDto>> GetPlaylistStreams(int id)
    // {
    //     IEnumerable<StreamDto> streamDtos = _mapper.Map<IEnumerable<StreamDto>>(streams);

    //     return streamDtos;
    // }
}
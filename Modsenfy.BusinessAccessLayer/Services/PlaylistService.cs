using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using Modsenfy.BusinessAccessLayer.DTOs;
using AutoMapper;

namespace Modsenfy.BusinessAccessLayer.Services
{
    public class PlaylistService
    {
        private readonly PlaylistRepository _playlistRepository;
        private readonly IMapper _mapper;

        public PlaylistService(PlaylistRepository playlistRepository, IMapper mapper)
        {
            _playlistRepository = playlistRepository;
            _mapper = mapper;
        }

        public async Task<PlaylistDto> GetPlaylist(int id)
        {
            var playlist = await _playlistRepository.GetByIdWithJoinsAsync(id);
            var playlistDto = _mapper.Map<PlaylistDto>(playlist);

            return playlistDto;
        }

        public async Task<PlaylistDto> CreatePlaylist(PlaylistDto playlistDto)
        {
            var playlist = _mapper.Map<Playlist>(playlistDto);

            await _playlistRepository.CreateAsync(playlist);
            await _playlistRepository.SaveChangesAsync();

            return playlistDto;
        }

        public async Task<PlaylistDto> UpdatePlaylist(PlaylistDto playlistDto)
        {
            var playlist = _mapper.Map<Playlist>(playlistDto);

            if (playlist is null)
            {
                return null;
            }

            await _playlistRepository.UpdateAsync(playlist);
            await _playlistRepository.SaveChangesAsync();

            return playlistDto;
        }

        public async Task<PlaylistDto> DeletePlaylist(int id)
        {
            var playlist = await _playlistRepository.GetByIdAsync(id);
            var playlistDto = _mapper.Map<PlaylistDto>(playlist);

            if (playlist is null)
            {
                return null;
            }

            _playlistRepository.Delete(playlist);
            await _playlistRepository.SaveChangesAsync();

            return playlistDto;
        }

        public async Task<IEnumerable<PlaylistDto>> GetSeveralPlaylists(List<int> ids)
        {
            var playlists = await _playlistRepository.GetSeveralPlaylistsAsync(ids);

            IEnumerable<PlaylistDto> playlistDtos = _mapper.Map<IEnumerable<PlaylistDto>>(playlists);

            return playlistDtos;
        }

        public async Task<IEnumerable<TrackDto>> GetTracksOfPlaylist(int id)
        {
            var playlist = await _playlistRepository.GetByIdWithJoinsAsync(id);
            var tracks = playlist.PlaylistTracks;

            IEnumerable<TrackDto> trackDtos = _mapper.Map<IEnumerable<TrackDto>>(tracks);

            return trackDtos;
        }
    }
}
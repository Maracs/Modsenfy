using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;
using Modsenfy.BusinessAccessLayer.Services;

namespace Modsenfy.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly PlaylistRepository _playlistRepository;
        private readonly PlaylistService _playlistService;
        private readonly IMapper _mapper;

        public PlaylistController(PlaylistRepository playlistRepository, PlaylistService playlistService, IMapper mapper)
        {
            _playlistRepository = playlistRepository;
            _playlistService = playlistService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistDto>> GetArtist(int id)
        {
            var playlist = await _playlistRepository.GetByIdWithJoins(id);

            if (playlist is null)
            {
                return BadRequest();
            }

            var playlistDto = _mapper.Map<ArtistDto>(playlist);

            return Ok(playlistDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlaylist(PlaylistDto playlistDto)
        {
            var playlist = _mapper.Map<Playlist>(playlistDto);

            if (playlist is null)
            {
                return BadRequest();
            }

            await _playlistRepository.Create(playlist);
            await _playlistRepository.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlaylist(PlaylistDto playlistDto)
        {
            var playlist = _mapper.Map<Playlist>(playlistDto);

            if (playlist is null)
            {
                return BadRequest();
            }
            
            await _playlistRepository.Update(playlist);
            await _playlistRepository.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlaylist(int id)
        {
            var playlist = await _playlistRepository.GetById(id);

            if (playlist is null)
            {
                return BadRequest();
            }
            
            _playlistRepository.Delete(playlist);
            await _playlistRepository.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetSeveralPlaylists(List<int> ids)
        {
            var playlistDtos = await _playlistService.GetSeveralPlaylists(ids);

            if (playlistDtos is null)
            {
                return BadRequest();
            }

            return Ok(playlistDtos);
        }

        [HttpGet("{id}/tracks")]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetTracksOfPlaylist(int id)
        {
            var trackDtos = await _playlistService.GetTracksOfPlaylist(id);

            if (trackDtos is null)
            {
                return BadRequest();
            }

            return Ok(trackDtos);
        }
    }
}
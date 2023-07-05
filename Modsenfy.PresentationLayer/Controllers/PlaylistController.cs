using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.Services;

namespace Modsenfy.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly PlaylistService _playlistService;
        private readonly IMapper _mapper;

        public PlaylistController(PlaylistService playlistService, IMapper mapper)
        {
            _playlistService = playlistService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistDto>> GetPlaylist(int id)
        {
            var playlistDto = await _playlistService.GetPlaylist(id);

            if (playlistDto is null)
            {
                return BadRequest();
            }

            return Ok(playlistDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlaylist(PlaylistDto playlistDto)
        {
            playlistDto = await _playlistService.CreatePlaylist(playlistDto);

            if (playlistDto is null)
            {
                return BadRequest();
            }

            return Ok(playlistDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlaylist(PlaylistDto playlistDto)
        {
            playlistDto = await _playlistService.UpdatePlaylist(playlistDto);

            if (playlistDto is null)
            {
                return BadRequest();
            }

            return Ok(playlistDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlaylist(int id)
        {
            var playlistDto = await _playlistService.DeletePlaylist(id);

            if (playlistDto is null)
            {
                return BadRequest();
            }

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
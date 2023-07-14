using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistDto>> GetPlaylist(int id)
        {
            var playlistDto = await _playlistService.GetPlaylistAsync(id);

            if (playlistDto is null)
            {
                return BadRequest();
            }

            return Ok(playlistDto);
        }

        [Authorize(Roles = "User,Artist")]
        [HttpPost]
        public async Task<ActionResult> CreatePlaylist(PlaylistDto playlistDto)
        {
            playlistDto = await _playlistService.CreatePlaylistAsync(playlistDto);

            if (playlistDto is null)
            {
                return BadRequest();
            }

            return Ok(playlistDto);
        }

        [Authorize(Roles = "User,Artist")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlaylist(PlaylistDto playlistDto)
        {
            playlistDto = await _playlistService.UpdatePlaylistAsync(playlistDto);

            if (playlistDto is null)
            {
                return BadRequest();
            }

            return Ok(playlistDto);
        }

        [Authorize(Roles = "User,Artist")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlaylist(int id)
        {
            var playlistDto = await _playlistService.DeletePlaylistAsync(id);

            if (playlistDto is null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetSeveralPlaylists(List<int> ids)
        {
            var playlistDtos = await _playlistService.GetSeveralPlaylistsAsync(ids);

            if (playlistDtos is null)
            {
                return BadRequest();
            }

            return Ok(playlistDtos);
        }

        [AllowAnonymous]
        [HttpGet("{id}/tracks")]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetTracksOfPlaylist(int id)
        {
            var trackDtos = await _playlistService.GetTracksOfPlaylistAsync(id);

            if (trackDtos is null)
            {
                return BadRequest();
            }

            return Ok(trackDtos);
        }
    }
}
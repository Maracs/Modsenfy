using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.BusinessAccessLayer.Services;

namespace Modsenfy.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistService _artistService;
        private readonly IMapper _mapper;
        public ArtistController(ArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDto>> GetArtist(int id)
        {
            var artistDto = await _artistService.GetArtistAsync(id);

            if (artistDto is null)
            {
                return BadRequest();
            }

            return Ok(artistDto);
        }

        // [Authorize(Roles = "???")]
        [HttpPost]
        public async Task<ActionResult<ArtistDto>> CreateArtist(ArtistDto artistDto)
        {
            artistDto = await _artistService.CreateArtistAsync(artistDto);

            if (artistDto is null)
            {
                return BadRequest();
            }
            
            return Ok(artistDto);
        }

        // [Authorize(Roles = "???")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ArtistDto>> UpdateArtist(ArtistDto artistDto)
        {
            artistDto = await _artistService.UpdateArtistAsync(artistDto);

            if (artistDto is null)
            {
                return BadRequest();
            }

            return Ok(artistDto);
        }

        // [Authorize(Roles = "???")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artistDto = await _artistService.DeleteArtistAsync(id);

            if (artistDto is null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetSeveralArtists(List<int> ids)
        {
            var artistDtos = await _artistService.GetSeveralArtistsAsync(ids);

            if (artistDtos is null)
            {
                return BadRequest();
            }

            return Ok(artistDtos);
        }

        [AllowAnonymous]
        [HttpGet("{id}/albums")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetArtistAlbums(int id)
        {
            var albumDtos = await _artistService.GetArtistAlbumsAsync(id);

            if (albumDtos is null)
            {
                return BadRequest();
            }

            return Ok(albumDtos);
        }

        [AllowAnonymous]
        [HttpGet("{id}/tracks")]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetArtistTracks(int id)
        {
            var trackDtos = await _artistService.GetArtistTracksAsync(id);

            if (trackDtos is null)
            {
                return BadRequest();
            }

            return Ok(trackDtos);
        }

        [AllowAnonymous]
        [HttpGet("{id}/streams")]
        public async Task<ActionResult<IEnumerable<StreamDto>>> GetArtistStreams(int id)
        {
        var streamDtos = await _artistService.GetArtistStreamsAsync(id);

        if (streamDtos is null)
        {
            return BadRequest();
        }

        return Ok(streamDtos);
        }
    }
}
using AutoMapper;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDto>> GetArtist(int id)
        {
            var artistDto = await _artistService.GetArtist(id);

            if (artistDto is null)
            {
                return BadRequest();
            }

            return Ok(artistDto);
        }

        [HttpPost]
        public async Task<ActionResult<ArtistDto>> CreateArtist(ArtistDto artistDto)
        {
            artistDto = await _artistService.CreateArtist(artistDto);

            if (artistDto is null)
            {
                return BadRequest();
            }

            return Ok(artistDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArtistDto>> UpdateArtist(ArtistDto artistDto)
        {
            artistDto = await _artistService.UpdateArtist(artistDto);

            if (artistDto is null)
            {
                return BadRequest();
            }

            return Ok(artistDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artistDto = await _artistService.DeleteArtist(id);

            if (artistDto is null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetSeveralArtists(List<int> ids)
        {
            var artistDtos = await _artistService.GetSeveralArtists(ids);

            if (artistDtos is null)
            {
                return BadRequest();
            }

            return Ok(artistDtos);
        }

        [HttpGet("{id}/albums")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetArtistAlbums(int id)
        {
            var albumDtos = await _artistService.GetArtistAlbums(id);

            if (albumDtos is null)
            {
                return BadRequest();
            }

            return Ok(albumDtos);
        }

        [HttpGet("{id}/tracks")]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetArtistTracks(int id)
        {
            var trackDtos = await _artistService.GetArtistTracks(id);

            if (trackDtos is null)
            {
                return BadRequest();
            }

            return Ok(trackDtos);
        }

        [HttpGet("{id}/streams")]
		public async Task<ActionResult<IEnumerable<StreamDto>>> GetAlbumStreams(int id)
		{
            var streamDtos = await _artistService.GetArtistStreams(id);

            if (streamDtos is null)
            {
                return BadRequest();
            }

            return Ok(streamDtos);
		}
    }
}
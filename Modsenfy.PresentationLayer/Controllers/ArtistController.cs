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
    public class ArtistController : ControllerBase
    {
        private readonly ArtistRepository _artistRepository;
        private readonly IMapper _mapper;
        private readonly ArtistService _artistService;

        public ArtistController(IMapper mapper, ArtistRepository artistRepository, ArtistService artistService)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
            _artistService = artistService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDto>> GetArtist(int id)
        {
            var artist = await _artistRepository.GetByIdWithJoins(id);

            if (artist is null)
            {
                return BadRequest();
            }

            var artistDto = _mapper.Map<ArtistDto>(artist);

            return Ok(artistDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateArtist(ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);

            if (artist is null)
            {
                return BadRequest();
            }

            await _artistRepository.Create(artist);
            await _artistRepository.SaveChanges();

            return Ok(artist.ArtistId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateArtist(ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);

            if (artist is null)
            {
                return BadRequest();
            }
            
            await _artistRepository.Update(artist);
            await _artistRepository.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artist = await _artistRepository.GetById(id);

            if (artist is null)
            {
                return BadRequest();
            }
            
            _artistRepository.Delete(artist);
            await _artistRepository.SaveChanges();

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
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modsenfy.BusinessAccessLayer.DTOs;
using Modsenfy.DataAccessLayer.Entities;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.PresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public ArtistController(IMapper mapper, ArtistRepository artistRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
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

            await _artistRepository.CreateAsync(artist);
            await _artistRepository.SaveChangesAsync();

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
            
            await _artistRepository.UpdateAsync(artist);
            await _artistRepository.SaveChangesAsync();

            return Ok(artist.ArtistId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);

            if (artist is null)
            {
                return BadRequest();
            }
            
            _artistRepository.Delete(artist);
            await _artistRepository.SaveChangesAsync();

            return Ok(artist.ArtistId);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetSeveralArtists(List<int> ids)
        {
            var artists = await _artistRepository.GetSeveralArtists(ids); 

            if (artists is null)
            {
                return BadRequest();
            }

            IEnumerable<ArtistDto> artistDtos = _mapper.Map<IEnumerable<ArtistDto>>(artists);
            
            return Ok(artistDtos);
        }

        [HttpGet("{id}/albums")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetArtistAlbums(int id)
        {
            var albums = await _artistRepository.GetArtistAlbums(id);

            if (albums is null)
            {
                return BadRequest();
            }

            IEnumerable<AlbumDto> albumDtos = _mapper.Map<IEnumerable<AlbumDto>>(albums);

            return Ok(albumDtos);
        }

        [HttpGet("{id}/tracks")]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetArtistTracks(int id)
        {
            var tracks = await _artistRepository.GetArtistTracks(id);

            if (tracks is null)
            {
                return BadRequest();
            }

            IEnumerable<TrackDto> trackDtos = _mapper.Map<IEnumerable<TrackDto>>(tracks);

            return Ok(trackDtos);
        }

        [HttpGet("{id}/streams")]
		public async Task<ActionResult<IEnumerable<StreamDto>>> GetAlbumStreams(int id)
		{
            var streams = await _artistRepository.GetArtistStreams(id);

            if (streams is null)
            {
                return BadRequest();
            }

            IEnumerable<StreamDto> streamDtos = _mapper.Map<IEnumerable<StreamDto>>(streams);

            return Ok(streamDtos);
		}
    }
}